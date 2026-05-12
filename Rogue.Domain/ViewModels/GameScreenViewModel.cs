using Rogue.Domain.Game;

namespace Rogue.Domain.ViewModels;

/// <summary>
/// Данные для отрисовки игрового экрана.
/// </summary>
public class GameScreenViewModel
{
    /// <summary>Тайлы карты для отрисовки.</summary>
    public TileViewModel[,] Tiles { get; set; } = null!;

    /// <summary>Статистика игрока для отображения.</summary>
    public string[] PlayerStats { get; set; } = [];

    /// <summary>Статистика игровой сессии.</summary>
    public GameStatistics SessionStatistics { get; set; } = null!;

    /// <summary>Сообщения лога за ход.</summary>
    public List<string> SessionLogMessages { get; set; } = [];

    /// <summary>Жив ли игрок.</summary>
    public bool IsAlive { get; set; }

    /// <summary>Достигнута ли победа в сессии.</summary>
    public bool SessionVictory { get; set; }
}