namespace Rogue.Domain.Entities.Items.Weapons.Interfaces;

/// <summary>
/// Лёгкое оружие: множитель силы ×2, множитель ловкости ×4.
/// Высокая точность, низкий урон.
/// </summary>
internal interface ILightWeapon
{
    int StrengthMultiplier => 2;
    int AgilityMultiplier => 4;
}

/// <summary>
/// Сбалансированное оружие: множитель силы ×3, множитель ловкости ×2.
/// Золотая середина.
/// </summary>
internal interface IBalancedWeapon
{
    int StrengthMultiplier => 3;
    int AgilityMultiplier => 2;
}

/// <summary>
/// Тяжёлое оружие: множитель силы ×4, множитель ловкости ×1.
/// Огромный урон, низкая точность.
/// </summary>
internal interface IHeavyWeapon
{
    int StrengthMultiplier => 4;
    int AgilityMultiplier => 1;
}