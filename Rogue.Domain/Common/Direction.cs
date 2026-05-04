namespace Rogue.Domain.Common;

/// <summary>
/// Направления движения: 4 ортогональных (по сторонам света) и 4 диагональных.
/// Используются игроком, монстрами и системой ввода.
/// </summary>
public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}