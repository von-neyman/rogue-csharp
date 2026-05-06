using Rogue.Domain.Entities.Items.Weapons;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо имеет инвентарь для хранения предметов.</summary>
public interface IInventory
{
    Inventory Inventory { get; set; }
}

/// <summary>Существо может носить оружие (и броню в будущем).</summary>
public interface IEquipment
{
    Weapon? EquippedWeapon { get; set; }
}

/// <summary>Существо преследует игрока в указанном радиусе.</summary>
public interface IHostility
{
    int Hostility { get; set; }
}

/// <summary>С существа выпадает добыча при смерти.</summary>
public interface ILoot
{
    Treasure? TreasureLoot { get; set; }
}

/// <summary>Существо имеет стоимость в очках пула монстров.</summary>
public interface ICost
{
    int Cost { get; set; }
}