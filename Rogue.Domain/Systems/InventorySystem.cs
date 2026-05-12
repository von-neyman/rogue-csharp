using Rogue.Domain.Entities;
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
internal static class InventorySystem
{
    /// <summary>Событие: игрок подобрал сокровище.</summary>
    internal static event Action? OnTreasureCollected;

    /// <summary>Подобрать все предметы с клетки, на которой стоит существо.</summary>
    internal static void CollectItems(Creature creature)
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
                case Treasure treasure:
                    inventory.Inventory.AddTreasure(treasure);
                    OnTreasureCollected?.Invoke();
                    break;
            }
        }
    }

    /// <summary>Использовать предмет указанного типа из инвентаря по индексу (1-9).</summary>
    internal static bool UseItem<T>(Creature creature, int slotIndex) where T : Item
    {
        if (typeof(T) == typeof(Food)) return UseFood(creature, slotIndex);
        if (typeof(T) == typeof(Potion)) return UsePotion(creature, slotIndex);
        if (typeof(T) == typeof(Scroll)) return UseScroll(creature, slotIndex);
        if (typeof(T) == typeof(Weapon)) return EquipWeapon(creature, slotIndex);
        return false;
    }

    /// <summary>Выбросить предмет указанного типа из инвентаря по индексу (1-9).</summary>
    internal static bool DropItem<T>(Creature creature, int slotIndex) where T : Item
    {
        if (creature is not IInventory inventory) return false;
        var list = GetItemsList<T>(inventory.Inventory);
        if (list == null) return false;
        if (slotIndex < 1 || slotIndex > list.Count) return false;
        var item = list[slotIndex - 1];
        list.RemoveAt(slotIndex - 1);
        PlaceOnGround(item, creature);
        return true;
    }

    /// <summary>Использовать еду из инвентаря по индексу (1-9).</summary>
    private static bool UseFood(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Foods.Count) return false;
        var food = inventory.Inventory.Foods[slotIndex - 1];
        creature.Heal(food.CalculateHealing(creature.MaxHealth));
        inventory.Inventory.Foods.RemoveAt(slotIndex - 1);
        return true;
    }

    /// <summary>Использовать зелье из инвентаря по индексу (1-9).</summary>
    private static bool UsePotion(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Potions.Count) return false;
        var potion = inventory.Inventory.Potions[slotIndex - 1];
        potion.Apply(creature);
        inventory.Inventory.Potions.RemoveAt(slotIndex - 1);
        return true;
    }

    /// <summary>Использовать свиток из инвентаря по индексу (1-9).</summary>
    private static bool UseScroll(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Scrolls.Count) return false;
        var scroll = inventory.Inventory.Scrolls[slotIndex - 1];
        scroll.Apply(creature);
        inventory.Inventory.Scrolls.RemoveAt(slotIndex - 1);
        return true;
    }

    /// <summary>Экипировать оружие из инвентаря по индексу (1-9) или убрать оружие (0).</summary>
    private static bool EquipWeapon(Creature creature, int slotIndex)
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

    /// <summary>Получить список предметов нужного типа из инвентаря.</summary>
    private static List<T>? GetItemsList<T>(Inventory inventory) where T : Item
    {
        if (typeof(T) == typeof(Food)) return inventory.Foods as List<T>;
        if (typeof(T) == typeof(Potion)) return inventory.Potions as List<T>;
        if (typeof(T) == typeof(Scroll)) return inventory.Scrolls as List<T>;
        if (typeof(T) == typeof(Weapon)) return inventory.Weapons as List<T>;
        if (typeof(T) == typeof(Treasure)) return inventory.Treasures as List<T>;
        return null;
    }

    /// <summary>Положить предмет на пол рядом с существом, а если некуда — под ним.</summary>
    private static void PlaceOnGround(Item item, Creature creature)
    {
        var currentTile = creature.CurrentTile;
        if (currentTile == null) return;
        var directions = new (int dx, int dy)[] { (0, -1), (0, 1), (-1, 0), (1, 0), (-1, -1), (-1, 1), (1, -1), (1, 1) };
        foreach (var (dx, dy) in directions)
        {
            int targetX = currentTile.X + dx;
            int targetY = currentTile.Y + dy;
            var level = currentTile.Level;
            if (level?.Map.IsWalkable(targetX, targetY) == true)
            {
                var tile = level.Map.GetTile(targetX, targetY);
                item.CurrentTile = tile;
                tile.ItemsOnTile.Add(item);
                return;
            }
        }
        item.CurrentTile = currentTile;
        currentTile.ItemsOnTile.Add(item);
    }

    /*
    /// <summary>Положить предмет на клетку, где стоит существо.</summary>
    private static void PlaceOnGround(Item item, Creature creature)
    {
        item.CurrentTile = creature.CurrentTile;
        creature.CurrentTile?.ItemsOnTile.Add(item);
    }
    */

    /// <summary>Выбросить сокровище существа при смерти.</summary>
    internal static void DropLoot(Creature creature)
    {
        if (creature is not ILoot loot || loot.TreasureLoot == null) return;
        loot.TreasureLoot.CurrentTile = creature.CurrentTile;
        creature.CurrentTile?.ItemsOnTile.Add(loot.TreasureLoot);
        creature.CurrentTile?.Level?.Items.Add(loot.TreasureLoot);
        loot.TreasureLoot = null;
    }
}