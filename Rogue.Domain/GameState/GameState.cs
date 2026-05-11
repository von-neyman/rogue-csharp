using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;
using Rogue.Domain.World;

namespace Rogue.Domain.GameState;

/// <summary>
/// Состояние игры. Хранит текущий уровень, игрока, статистику и управляет игровым циклом.
/// </summary>
public class GameState
{
    /// <summary>Событие: игрок перешёл на новый уровень.</summary>
    public static event Action? OnLevelChanged;

    /// <summary>Текущий игрок.</summary>
    public Hero Player { get; set; }

    /// <summary>Текущий уровень.</summary>
    public Level CurrentLevel { get; set; }

    /// <summary>Статистика игры.</summary>
    public GameStatistics Statistics { get; set; }

    public GameState()
    {
        Player = new Hero();
        Statistics = new GameStatistics();
        CurrentLevel = LevelGenerator.Generate(1);
        SubscribeToEvents();
        PlacePlayerOnLevel();
    }

    /// <summary>
    /// Выполнить игровое действие и обработать ход мира.
    /// </summary>
    public void DoAction(GameAction gameAction)
    {
        if (!ActionSystem.CreatureAction(Player, ref gameAction)) return;
        ActionSystem.MonstersAction(CurrentLevel);
        CheckLevelTransition();
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
            // TODO: победа
            return;
        }
        CurrentLevel = LevelGenerator.Generate(nextLevel);
        PlacePlayerOnLevel();
    }

    /// <summary>Разместить игрока на стартовой точке уровня.</summary>
    private void PlacePlayerOnLevel()
    {
        var startTile = CurrentLevel.Map.GetTile(CurrentLevel.StartPoint.X, CurrentLevel.StartPoint.Y);
        Player.CurrentTile = startTile;
        startTile.CreaturesOnTile.Add(Player);
        CurrentLevel.Hero = Player;
        OnLevelChanged?.Invoke();
    }
}