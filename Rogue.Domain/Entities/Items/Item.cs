namespace Rogue.Domain.Entities.Items;

/// <summary>
/// Базовый класс для всех предметов в игре.
/// Хранит общие свойства: название, описание, позицию на карте и символ для отображения.
/// </summary>
public abstract class Item
{
    /// <summary>Название предмета.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Художественное описание.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Находится ли предмет на полу (true) или в инвентаре (false).</summary>
    public bool IsOnGround { get; set; } = true;

    /// <summary>Координата X на карте (если на полу).</summary>
    public int X { get; set; }

    /// <summary>Координата Y на карте (если на полу).</summary>
    public int Y { get; set; }

    /// <summary>Символ для отображения на карте.</summary>
    public char Symbol { get; set; }
}