using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Weapons;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Главный герой. Управляется игроком.
/// Стартовые характеристики: базовая сила 4, базовая ловкость 4, базовое здоровье 20.
/// </summary>
public class Hero : Creature, IInventory, IEquipment
{
    /// <summary>Инвентарь с предметами.</summary>
    public Inventory Inventory { get; set; }

    /// <summary>Текущее оружие в руках (null — без оружия).</summary>
    public Weapon? EquippedWeapon { get; set; }

    public Hero()
    {
        BaseStrength = 4;
        BaseAgility = 4;
        BaseMaxHealth = 20;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Symbol = '@';
        Inventory = new Inventory();
    }
}