namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Базовый класс для оружия.
/// Конкретные свойства определяются интерфейсами веса (ILight/IBalanced/IHeavy)
/// и механик (ISwiftStrike/ICrit/ICounterattack/IParry).
/// </summary>
public abstract class Weapon : Item { }