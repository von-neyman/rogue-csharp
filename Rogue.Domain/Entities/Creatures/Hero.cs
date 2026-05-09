using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Weapons;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Главный герой. Управляется игроком.
/// </summary>
public class Hero : Creature, IInventory, IEquipment, ICanMove, ICanAttack
{
    /// <summary>Инвентарь с предметами.</summary>
    public Inventory Inventory { get; set; }

    /// <summary>Текущее оружие в руках (null — без оружия).</summary>
    public Weapon? EquippedWeapon { get; set; }

    public Hero()
    {
        Name = "Герой";
        Description = "Искатель приключений.";
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

    /// <summary>Атаковать другое существо.</summary>
    public bool Attack(Creature target) => CombatSystem.Attack(this, target);

    /// <summary>Совершить движение в указанном направлении.</summary>
    public bool Move(Direction direction) => MovementSystem.PerformAction(this, direction);
}