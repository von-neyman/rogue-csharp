namespace Rogue.Domain.Common;

/// <summary>
/// Направления движения: бездействие, 4 ортогональных и 4 диагональных.
/// Используются игроком, монстрами и системой ввода.
/// </summary>
public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}