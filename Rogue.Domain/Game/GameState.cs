using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;
using Rogue.Domain.ViewModels;
using Rogue.Domain.World;

namespace Rogue.Domain.Game;

/// <summary>
/// Состояние игры. Хранит текущий уровень, игрока, статистику и управляет игровым циклом.
/// </summary>
public class GameState
{
    /// <summary>Событие: игрок перешёл на новый уровень.</summary>
    internal static event Action? OnLevelChanged;

    /// <summary>Текущий игрок.</summary>
    internal Hero Player { get; set; }

    /// <summary>Текущий уровень.</summary>
    internal Level CurrentLevel { get; set; }

    /// <summary>Статистика игры.</summary>
    internal GameStatistics Statistics { get; set; }

    /// <summary>Достигнута ли победа.</summary>
    internal bool IsVictory { get; set; }

    /// <summary>Вышел ли игрок из игры с сохранением.</summary>
    internal bool IsQuit { get; set; }

    /// <summary>Сообщения лога за текущий ход.</summary>
    internal List<string> LogMessages { get; set; } = [];

    public GameState()
    {
        Player = new Hero();
        Statistics = new GameStatistics();
        CurrentLevel = LevelGenerator.Generate(1);
        SubscribeToEvents();
        PlacePlayerOnLevel();
    }

    /// <summary>Сбросить состояние выхода после загрузки сохранённой игры.</summary>
    public void ResetAfterLoad()
    {
        IsQuit = false;
    }

    /// <summary>
    /// Выполнить игровое действие и обработать ход мира.
    /// </summary>
    public void DoAction(GameAction gameAction)
    {
        if (!Player.IsAlive || IsVictory) return;
        if (CheckGameQuit(gameAction)) return;
        if (!ActionSystem.CreatureAction(Player, ref gameAction)) return;
        FogOfWarSystem.UpdateVisibility(CurrentLevel);
        ActionSystem.MonstersAction(CurrentLevel);
        CheckLevelTransition();
        CheckGameEnd();
    }

    /// <summary>Собрать ViewModel для отрисовки.</summary>
    public GameScreenViewModel GetViewModel()
    {
        var viewModel = new GameScreenViewModel
        {
            Tiles = BuildTileViewModels(),
            PlayerStats = BuildPlayerStats(),
            SessionStatistics = Statistics,
            SessionLogMessages = LogMessages,
            IsAlive = Player.IsAlive,
            SessionVictory = IsVictory,
            SessionQuit = IsQuit
        };
        return viewModel;
    }

    /// <summary>Собрать ViewModel для отображения инвентаря.</summary>
    public InventoryViewModel GetInventoryViewModel()
    {
        return new InventoryViewModel
        {
            FoodItems = Player.Inventory.Foods.Select(f => $"{f.Name}. {f.ShortDescription}").ToList(),
            PotionItems = Player.Inventory.Potions.Select(p => $"{p.Name}. {p.ShortDescription}").ToList(),
            ScrollItems = Player.Inventory.Scrolls.Select(s => $"{s.Name}. {s.ShortDescription}").ToList(),
            WeaponItems = Player.Inventory.Weapons.Select(w => $"{w.Name}. {w.ShortDescription}").ToList(),
            TreasureItems = Player.Inventory.Treasures.Select(t => $"{t.Name}. {t.ShortDescription}").ToList()
        };
    }

    /// <summary>Построить матрицу TileViewModel для отрисовки карты.</summary>
    private TileViewModel[,] BuildTileViewModels()
    {
        var tiles = new TileViewModel[Map.Height, Map.Width];
        for (int y = 0; y < Map.Height; y++)
            for (int x = 0; x < Map.Width; x++)
            {
                var tile = CurrentLevel.Map.Tiles[y, x];
                tiles[y, x] = new TileViewModel
                {
                    Symbol = GetDisplaySymbol(tile),
                    IsVisible = tile.IsVisible,
                    IsExplored = tile.IsExplored
                };
            }
        return tiles;
    }

    /// <summary>Определить символ для отображения на клетке.</summary>
    private static char GetDisplaySymbol(Tile tile)
    {
        if (!tile.IsVisible) return tile.IsExplored ? '#' : ' ';
        var aliveCreature = tile.CreaturesOnTile.FirstOrDefault(c => c.IsAlive);
        if (aliveCreature != null) return aliveCreature.Symbol;
        if (tile.ItemsOnTile.Count > 0) return tile.ItemsOnTile[^1].Symbol;
        return tile.Symbol;
    }

    /// <summary>Собрать массив строк статистики игрока.</summary>
    private string[] BuildPlayerStats()
    {
        return
        [
            $"Здоровье: {Player.Health}/{Player.MaxHealth}",
            $"Сила: {Player.Strength}",
            $"Ловкость: {Player.Agility}",
            $"Оружие: {(Player.EquippedWeapon?.Name ?? "Кулаки")}",
            $"Стоимость сокровищ: {Player.Inventory.TotalTreasureValue}",
            $"Уровень: {CurrentLevel.LevelNumber}"
        ];
    }

    /// <summary>Подписаться на события систем для обновления статистики.</summary>
    private void SubscribeToEvents()
    {
        CombatSystem.OnHitLanded += () => Statistics.HitsLanded++;
        CombatSystem.OnHitReceived += () => Statistics.HitsReceived++;
        CombatSystem.OnMonsterDefeated += () => Statistics.MonstersDefeated++;
        ActionSystem.OnFoodEaten += () => Statistics.FoodEaten++;
        ActionSystem.OnPotionUsed += () => Statistics.PotionsUsed++;
        ActionSystem.OnScrollRead += () => Statistics.ScrollsRead++;
        ActionSystem.OnStepTaken += () => Statistics.StepsTaken++;
        InventorySystem.OnTreasureCollected += () => Statistics.TreasureCollected = Player.Inventory.TotalTreasureValue;
        OnLevelChanged += () => Statistics.LevelReached = CurrentLevel.LevelNumber;
    }

    /// <summary>Проверить переход на следующий уровень.</summary>
    private void CheckLevelTransition()
    {
        if (Player.CurrentTile == null) return;
        if (Player.CurrentTile.X == CurrentLevel.ExitPoint.X && Player.CurrentTile.Y == CurrentLevel.ExitPoint.Y) AdvanceToNextLevel();
    }

    /// <summary>Переход на следующий уровень.</summary>
    private void AdvanceToNextLevel()
    {
        int nextLevel = CurrentLevel.LevelNumber + 1;
        if (nextLevel > 21)
        {
            IsVictory = true;
            return;
        }
        CurrentLevel = LevelGenerator.Generate(nextLevel);
        PlacePlayerOnLevel();
    }

    /// <summary>Разместить игрока на стартовой точке уровня.</summary>
    private void PlacePlayerOnLevel()
    {
        var startTile = CurrentLevel.Map.GetTile(CurrentLevel.StartPoint.X, CurrentLevel.StartPoint.Y);
        if (startTile == null) return;
        Player.CurrentTile = startTile;
        startTile.CreaturesOnTile.Add(Player);
        CurrentLevel.Hero = Player;
        CurrentLevel.HeroParty.Add(Player);
        OnLevelChanged?.Invoke();
    }

    /// <summary>Проверить окончание игры.</summary>
    private void CheckGameEnd()
    {
        if (!Player.IsAlive || IsVictory)
        {
            // TODO: ISaveLoadService.SaveLeaderboard(Statistics)
        }
    }

    /// <summary>Проверить выход из игры с сохранением. Возвращает true, если игрок вышел.</summary>
    private bool CheckGameQuit(GameAction gameAction)
    {
        if (gameAction != GameAction.Quit) return false;
        IsQuit = true;
        // TODO: ISaveLoadService.Save(this)
        // TODO: ISaveLoadService.SaveLeaderboard(Statistics)
        return true;
    }
}