using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Items;

namespace Rogue.Domain.World;

/// <summary>
/// Одна клетка карты. Хранит координаты, символ для отрисовки, флаги проходимости,
/// прозрачности, информацию для тумана войны и списки сущностей на клетке.
/// </summary>
public class Tile
{
    /// <summary>Координата X клетки.</summary>
    public int X { get; set; }

    /// <summary>Координата Y клетки.</summary>
    public int Y { get; set; }

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

    /// <summary>Существа, находящиеся на этой клетке.</summary>
    public List<Creature> CreaturesOnTile { get; set; } = [];

    /// <summary>Предметы, лежащие на этой клетке.</summary>
    public List<Item> ItemsOnTile { get; set; } = [];
}