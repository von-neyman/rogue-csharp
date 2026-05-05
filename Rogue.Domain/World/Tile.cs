namespace Rogue.Domain.World;

/// <summary>
/// Одна клетка карты. Хранит символ для отрисовки, флаги проходимости,
/// прозрачности (для Ray Casting), а также информацию для тумана войны.
/// </summary>
public class Tile
{
    /// <summary>Символ для отображения на экране.</summary>
    public char Symbol { get; set; }

    /// <summary>Можно ли встать на эту клетку.</summary>
    public bool IsWalkable { get; set; }

    /// <summary>Проходит ли свет/взгляд через клетку (для Ray Casting и тумана войны).</summary>
    public bool IsTransparent { get; set; }

    /// <summary>Была ли клетка увидена игроком хотя бы раз (для отрисовки исследованного).</summary>
    public bool IsExplored { get; set; }

    /// <summary>Видна ли клетка прямо сейчас (в прямой видимости игрока).</summary>
    public bool IsVisible { get; set; }
}