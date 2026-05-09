using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items;
using Rogue.Domain.Entities.Items.Food;
using Rogue.Domain.Entities.Items.Potions;
using Rogue.Domain.Entities.Items.Scrolls;
using Rogue.Domain.Entities.Items.Treasures;
using Rogue.Domain.Entities.Items.Weapons;

namespace Rogue.Domain.Systems;

/// <summary>
/// Система инвентаря: подбор, использование и выбрасывание предметов.
/// </summary>
public static class InventorySystem
{
    /// <summary>Подобрать все предметы с клетки, на которой стоит существо.</summary>
    public static void CollectItems(Creature creature)
    {
        if (creature is not IInventory inventory) return;
        if (creature.CurrentTile?.ItemsOnTile == null) return;
        for (int i = creature.CurrentTile.ItemsOnTile.Count - 1; i >= 0; i--)
        {
            var item = creature.CurrentTile.ItemsOnTile[i];
            switch (item)
            {
                case Food food: inventory.Inventory.AddFood(food); break;
                case Weapon weapon: inventory.Inventory.AddWeapon(weapon); break;
                case Scroll scroll: inventory.Inventory.AddScroll(scroll); break;
                case Potion potion: inventory.Inventory.AddPotion(potion); break;
                case Treasure treasure: inventory.Inventory.AddTreasure(treasure); break;
            }
        }
    }

    /// <summary>Использовать еду из инвентаря по индексу (1-9).</summary>
    public static bool UseFood(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Foods.Count) return false;
        var food = inventory.Inventory.Foods[slotIndex - 1];
        creature.Heal(food.CalculateHealing(creature.MaxHealth));
        inventory.Inventory.Foods.RemoveAt(slotIndex - 1);
        return true;
    }

    /// <summary>Использовать зелье из инвентаря по индексу (1-9).</summary>
    public static bool UsePotion(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Potions.Count) return false;
        var potion = inventory.Inventory.Potions[slotIndex - 1];
        potion.Apply(creature);
        inventory.Inventory.Potions.RemoveAt(slotIndex - 1);
        return true;
    }

    /// <summary>Использовать свиток из инвентаря по индексу (1-9).</summary>
    public static bool UseScroll(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Scrolls.Count) return false;
        var scroll = inventory.Inventory.Scrolls[slotIndex - 1];
        scroll.Apply(creature);
        inventory.Inventory.Scrolls.RemoveAt(slotIndex - 1);
        return true;
    }

    /// <summary>Экипировать оружие из инвентаря по индексу (1-9) или убрать оружие (0).</summary>
    public static bool EquipWeapon(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory || creature is not IEquipment equipment) return false;
        if (slotIndex == 0)
        {
            if (equipment.EquippedWeapon == null) return false;
            if (!inventory.Inventory.AddWeapon(equipment.EquippedWeapon)) PlaceOnGround(equipment.EquippedWeapon, creature);
            equipment.EquippedWeapon = null;
            return true;
        }
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Weapons.Count) return false;
        var newWeapon = inventory.Inventory.Weapons[slotIndex - 1];
        if (equipment.EquippedWeapon != null) inventory.Inventory.Weapons[slotIndex - 1] = equipment.EquippedWeapon;
        else inventory.Inventory.Weapons.RemoveAt(slotIndex - 1);
        equipment.EquippedWeapon = newWeapon;
        return true;
    }

    /// <summary>Выбросить еду из инвентаря по индексу (1-9).</summary>
    public static bool DropFood(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Foods.Count) return false;
        var food = inventory.Inventory.Foods[slotIndex - 1];
        inventory.Inventory.Foods.RemoveAt(slotIndex - 1);
        PlaceOnGround(food, creature);
        return true;
    }

    /// <summary>Выбросить зелье из инвентаря по индексу (1-9).</summary>
    public static bool DropPotion(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Potions.Count) return false;
        var potion = inventory.Inventory.Potions[slotIndex - 1];
        inventory.Inventory.Potions.RemoveAt(slotIndex - 1);
        PlaceOnGround(potion, creature);
        return true;
    }

    /// <summary>Выбросить свиток из инвентаря по индексу (1-9).</summary>
    public static bool DropScroll(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Scrolls.Count) return false;
        var scroll = inventory.Inventory.Scrolls[slotIndex - 1];
        inventory.Inventory.Scrolls.RemoveAt(slotIndex - 1);
        PlaceOnGround(scroll, creature);
        return true;
    }

    /// <summary>Выбросить оружие из инвентаря по индексу (1-9).</summary>
    public static bool DropWeapon(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Weapons.Count) return false;
        var weapon = inventory.Inventory.Weapons[slotIndex - 1];
        inventory.Inventory.Weapons.RemoveAt(slotIndex - 1);
        PlaceOnGround(weapon, creature);
        return true;
    }

    /// <summary>Выбросить сокровище из инвентаря по индексу (1-N).</summary>
    public static bool DropTreasure(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Treasures.Count) return false;
        var treasure = inventory.Inventory.Treasures[slotIndex - 1];
        inventory.Inventory.Treasures.RemoveAt(slotIndex - 1);
        PlaceOnGround(treasure, creature);
        return true;
    }

    /// <summary>Положить предмет на клетку, где стоит существо.</summary>
    private static void PlaceOnGround(Item item, Creature creature)
    {
        item.CurrentTile = creature.CurrentTile;
        creature.CurrentTile?.ItemsOnTile.Add(item);
    }

    /// <summary>Выбросить сокровище существа при смерти.</summary>
    public static void DropLoot(Creature creature)
    {
        if (creature is not ILoot loot || loot.TreasureLoot == null) return;
        loot.TreasureLoot.CurrentTile = creature.CurrentTile;
        creature.CurrentTile?.ItemsOnTile.Add(loot.TreasureLoot);
        creature.CurrentTile?.Level?.Items.Add(loot.TreasureLoot);
        loot.TreasureLoot = null;
    }
}