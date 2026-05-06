using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Weapons;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Главный герой. Управляется игроком.
/// Стартовые характеристики: сила 4, ловкость 4, здоровье 20.
/// Может экипировать оружие и носить предметы в инвентаре.
/// </summary>
public class Hero : Creature, IInventory, IEquipment
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
}