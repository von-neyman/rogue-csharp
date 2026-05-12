namespace Rogue.Domain.World;

/// <summary>
/// Коридор, соединяющий две комнаты.
/// Состоит из цепочки точек излома. Может быть L-образным, Z-образным или прямым.
/// </summary>
internal class Corridor
{
    /// <summary>Тип коридора: 0 — слева направо, 1 — левый поворот, 2 — правый поворот, 3 — сверху вниз.</summary>
    internal int Type { get; set; }

    /// <summary>Точки излома коридора (от 3 до 4). Первая и последняя — дверные проёмы комнат.</summary>
    internal List<(int X, int Y)> Points { get; set; } = [];

    /// <summary>Количество точек излома.</summary>
    internal int PointsCount => Points.Count;
}