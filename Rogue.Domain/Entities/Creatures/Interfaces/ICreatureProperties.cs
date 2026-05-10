using Rogue.Domain.Entities.Items;
using Rogue.Domain.Entities.Items.Treasures;
using Rogue.Domain.Entities.Items.Weapons;

namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо имеет инвентарь для хранения предметов.</summary>
public interface IInventory
{
    Inventory Inventory { get; set; }
    void CollectItems();
    bool UseItem<T>(int slotIndex) where T : Item;
    bool DropItem<T>(int slotIndex) where T : Item;
}

/// <summary>Существо может держать в руках оружие.</summary>
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
    void DropLoot();
}

/// <summary>Существо имеет стоимость в очках пула монстров.</summary>
public interface ICost
{
    int Cost { get; set; }
}