using Rogue.Domain.Entities.Items.Weapons;
using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Главный герой. Управляется игроком.
/// Стартовые характеристики: сила 4, ловкость 4, здоровье 20.
/// Может экипировать оружие и носить предметы в инвентаре.
/// </summary>
public class Hero : Creature
{
    /// <summary>Инвентарь с предметами.</summary>
    public Inventory Inventory { get; set; }

    /// <summary>Текущее оружие в руках (null — без оружия).</summary>
    public Weapon? EquippedWeapon { get; set; }

    public Hero()
    {
        Strength = 4;
        Agility = 4;
        MaxHealth = 20;
        Health = 20;
        Symbol = '@';
        Inventory = new Inventory();
    }

    /// <summary>Рассчитать урон с учётом оружия. Без оружия — чистая сила.</summary>
    public int CalculateDamage()
    {
        int multiplier = 1;
        if (EquippedWeapon != null)
        {
            multiplier = EquippedWeapon switch
            {
                ILightWeapon w => w.StrengthMultiplier,
                IBalancedWeapon w => w.StrengthMultiplier,
                IHeavyWeapon w => w.StrengthMultiplier,
                _ => 1
            };
        }
        return Strength * multiplier;
    }

    /// <summary>Рассчитать эффективную ловкость для попадания с учётом оружия.</summary>
    public int CalculateAccuracy()
    {
        int multiplier = 1;
        if (EquippedWeapon != null)
        {
            multiplier = EquippedWeapon switch
            {
                ILightWeapon w => w.AgilityMultiplier,
                IBalancedWeapon w => w.AgilityMultiplier,
                IHeavyWeapon w => w.AgilityMultiplier,
                _ => 1
            };
        }
        return Agility * multiplier;
    }
}