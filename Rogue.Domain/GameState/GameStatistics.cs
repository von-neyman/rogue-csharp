namespace Rogue.Domain.GameState;

/// <summary>
/// Статистика игровой сессии. Собирается во время игры и отображается в таблице лидеров.
/// </summary>
public class GameStatistics
{
    /// <summary>Количество собранных сокровищ (в золотых).</summary>
    public int TreasureCollected { get; set; }

    /// <summary>Достигнутый уровень подземелья.</summary>
    public int LevelReached { get; set; }

    /// <summary>Количество побеждённых противников.</summary>
    public int MonstersDefeated { get; set; }

    /// <summary>Количество съеденной еды.</summary>
    public int FoodEaten { get; set; }

    /// <summary>Количество выпитых зелий.</summary>
    public int PotionsUsed { get; set; }

    /// <summary>Количество прочитанных свитков.</summary>
    public int ScrollsRead { get; set; }

    /// <summary>Количество нанесённых игроком ударов (попавших по врагам).</summary>
    public int HitsLanded { get; set; }

    /// <summary>Количество пропущенных игроком ударов (попавших по игроку).</summary>
    public int HitsReceived { get; set; }

    /// <summary>Количество пройденных клеток.</summary>
    public int StepsTaken { get; set; }
}