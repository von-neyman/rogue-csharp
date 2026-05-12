using Rogue.Domain.Entities.Items;
using Rogue.Domain.Entities.Items.Treasures;
using Rogue.Domain.Entities.Items.Weapons;

namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо имеет инвентарь для хранения предметов.</summary>
internal interface IInventory
{
    Inventory Inventory { get; set; }
    void CollectItems();
    bool UseItem<T>(int slotIndex) where T : Item;
    bool DropItem<T>(int slotIndex) where T : Item;
}

/// <summary>Существо может держать в руках оружие.</summary>
internal interface IEquipment
{
    Weapon? EquippedWeapon { get; set; }
}

/// <summary>Существо преследует игрока в указанном радиусе.</summary>
internal interface IHostility
{
    int Hostility { get; set; }
}

/// <summary>С существа выпадает добыча при смерти.</summary>
internal interface ILoot
{
    Treasure? TreasureLoot { get; set; }
    void DropLoot();
}

/// <summary>Существо имеет стоимость в очках пула монстров.</summary>
internal interface ICost
{
    int Cost { get; set; }
}