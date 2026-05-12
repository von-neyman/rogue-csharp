namespace Rogue.Domain.ViewModels;

/// <summary>
/// Данные одной клетки для отрисовки.
/// </summary>
public class TileViewModel
{
    /// <summary>Символ для отображения.</summary>
    public char Symbol { get; set; }

    /// <summary>Видна ли клетка.</summary>
    public bool IsVisible { get; set; }

    /// <summary>Была ли клетка исследована.</summary>
    public bool IsExplored { get; set; }
}