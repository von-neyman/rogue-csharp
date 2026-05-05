using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Items;

namespace Rogue.Domain.World;

/// <summary>
/// Комната подземелья. Прямоугольная область, ограниченная стенами.
/// Содержит списки монстров и предметов, а также координаты дверей.
/// </summary>
public class Room
{
    /// <summary>Номер сектора (0-8). Вычисляется как GridI * 3 + GridJ.</summary>
    public int Sector => GridI * 3 + GridJ;

    /// <summary>Индекс в сетке 3×3 по вертикали (0-2).</summary>
    public int GridI { get; set; }

    /// <summary>Индекс в сетке 3×3 по горизонтали (0-2).</summary>
    public int GridJ { get; set; }

    /// <summary>Левый верхний угол комнаты.</summary>
    public (int X, int Y) TopLeft { get; set; }

    /// <summary>Правый нижний угол комнаты.</summary>
    public (int X, int Y) BottomRight { get; set; }

    /// <summary>Координаты дверей: [0] верх, [1] право, [2] низ, [3] лево.
    /// Если двери нет — координаты (-1, -1).</summary>
    public (int X, int Y)[] Doors { get; set; } = new (int, int)[4];

    /// <summary>Связи с соседними комнатами: [0] верх, [1] право, [2] низ, [3] лево.
    /// null — связи нет.</summary>
    public Room?[] Connections { get; set; } = new Room?[4];

    /// <summary>Монстры, находящиеся в комнате.</summary>
    public List<Monster> Monsters { get; set; } = [];

    /// <summary>Предметы на полу в комнате.</summary>
    public List<Item> Items { get; set; } = [];

    /// <summary>Является ли комната стартовой (игрок появляется здесь).</summary>
    public bool IsStartRoom { get; set; }

    /// <summary>Является ли комната конечной (здесь выход на следующий уровень).</summary>
    public bool IsExitRoom { get; set; }

    /// <summary>Инициализация дверей значениями по умолчанию (-1, -1 — нет двери).</summary>
    public Room()
    {
        for (int i = 0; i < 4; i++)
            Doors[i] = (-1, -1);
    }
}