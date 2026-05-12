using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Items;

namespace Rogue.Domain.World;

/// <summary>
/// Один уровень (ярус) подземелья. Содержит карту, комнаты, коридоры,
/// списки монстров и предметов, героя, а также точки входа и выхода.
/// </summary>
internal class Level
{
    /// <summary>Номер уровня (1-21).</summary>
    internal int LevelNumber { get; set; }

    /// <summary>Карта уровня (матрица клеток).</summary>
    internal Map Map { get; set; } = new();

    /// <summary>Список всех комнат на уровне.</summary>
    internal List<Room> Rooms { get; set; } = [];

    /// <summary>Список всех коридоров на уровне.</summary>
    internal List<Corridor> Corridors { get; set; } = [];

    /// <summary>Герой на уровне.</summary>
    internal Hero? Hero { get; set; }

    /// <summary>Все монстры на уровне.</summary>
    internal List<Monster> Monsters { get; set; } = [];

    /// <summary>Все предметы.</summary>
    internal List<Item> Items { get; set; } = [];

    /// <summary>Точка входа игрока на уровень (X, Y).</summary>
    internal (int X, int Y) StartPoint { get; set; }

    /// <summary>Точка выхода на следующий уровень (X, Y).</summary>
    internal (int X, int Y) ExitPoint { get; set; }

    /// <summary>Пул очков монстров на этом уровне.</summary>
    internal int MonsterPool => 4 + (LevelNumber - 1) * 2;
}