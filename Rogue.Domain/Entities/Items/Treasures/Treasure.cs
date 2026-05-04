namespace Rogue.Domain.Entities.Items.Treasures;

/// <summary>
/// Базовый класс для сокровищ.
/// Несут ценность, влияющую на итоговый рейтинг.
/// Выпадают только при победе над монстром.
/// </summary>
public abstract class Treasure : Item
{
    /// <summary>Ценность в условных золотых монетах (1, 2 или 4).</summary>
    public abstract int Value { get; }
}