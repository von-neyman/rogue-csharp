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
    /// <summary>Событие: запись в лог.</summary>
    internal static event Action<string>? OnLogMessage;

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
            bool added = false;
            switch (item)
            {
                case Food food:
                    added = inventory.Inventory.AddFood(food);
                    if (added) LogInventory($"{creature.Name} подобрал {food.NameAccusative}. {food.Description}");
                    else LogInventory($"{creature.Name} попытался подобрать {food.NameAccusative}, но в рюкзаке нет места.");
                    break;
                case Weapon weapon:
                    added = inventory.Inventory.AddWeapon(weapon);
                    if (added) LogInventory($"{creature.Name} подобрал {weapon.NameAccusative}. {weapon.Description}");
                    else LogInventory($"{creature.Name} попытался подобрать {weapon.NameAccusative}, но в рюкзаке нет места.");
                    break;
                case Scroll scroll:
                    added = inventory.Inventory.AddScroll(scroll);
                    if (added) LogInventory($"{creature.Name} подобрал {scroll.NameAccusative}. {scroll.Description}");
                    else LogInventory($"{creature.Name} попытался подобрать {scroll.NameAccusative}, но в рюкзаке нет места.");
                    break;
                case Potion potion:
                    added = inventory.Inventory.AddPotion(potion);
                    if (added) LogInventory($"{creature.Name} подобрал {potion.NameAccusative}. {potion.Description}");
                    else LogInventory($"{creature.Name} попытался подобрать {potion.NameAccusative}, но в рюкзаке нет места.");
                    break;
                case Treasure treasure:
                    inventory.Inventory.AddTreasure(treasure);
                    LogInventory($"{creature.Name} подобрал {treasure.NameAccusative}. {treasure.Description}");
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
        LogInventory($"{creature.Name} выбросил {item.NameAccusative}.");
        return true;
    }

    /// <summary>Выбросить сокровище существа при смерти.</summary>
    internal static void DropLoot(Creature creature)
    {
        if (creature is not ILoot loot || loot.TreasureLoot == null) return;
        loot.TreasureLoot.CurrentTile = creature.CurrentTile;
        creature.CurrentTile?.ItemsOnTile.Add(loot.TreasureLoot);
        creature.CurrentTile?.Level?.Items.Add(loot.TreasureLoot);
        loot.TreasureLoot = null;
    }

    /// <summary>Записать сообщение в лог.</summary>
    private static void LogInventory(string message) => OnLogMessage?.Invoke(message);

    /// <summary>Использовать еду из инвентаря по индексу (1-9).</summary>
    private static bool UseFood(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory) return false;
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Foods.Count) return false;
        var food = inventory.Inventory.Foods[slotIndex - 1];
        int healing = food.CalculateHealing(creature.MaxHealth);
        creature.Heal(healing);
        inventory.Inventory.Foods.RemoveAt(slotIndex - 1);
        LogInventory($"{creature.Name} съел {food.NameAccusative}. Восстановлено {healing} здоровья.");
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
        LogInventory($"{creature.Name} выпил {potion.NameAccusative}. Эффект продлится {Potion.Duration} ходов.");
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
        LogInventory($"{creature.Name} прочитал {scroll.NameAccusative}. Характеристика увеличена.");
        return true;
    }

    /// <summary>Экипировать оружие из инвентаря по индексу (1-9) или убрать оружие (0).</summary>
    private static bool EquipWeapon(Creature creature, int slotIndex)
    {
        if (creature is not IInventory inventory || creature is not IEquipment equipment) return false;
        if (slotIndex == 0)
        {
            if (equipment.EquippedWeapon == null) return false;
            if (!inventory.Inventory.AddWeapon(equipment.EquippedWeapon))
            {
                PlaceOnGround(equipment.EquippedWeapon, creature);
                LogInventory($"{creature.Name} выбросил {equipment.EquippedWeapon.NameAccusative} на пол.");
            }
            else LogInventory($"{creature.Name} убрал {equipment.EquippedWeapon.NameAccusative} в рюкзак.");
            equipment.EquippedWeapon = null;
            return true;
        }
        if (slotIndex < 1 || slotIndex > inventory.Inventory.Weapons.Count) return false;
        var newWeapon = inventory.Inventory.Weapons[slotIndex - 1];
        if (equipment.EquippedWeapon != null) inventory.Inventory.Weapons[slotIndex - 1] = equipment.EquippedWeapon;
        else inventory.Inventory.Weapons.RemoveAt(slotIndex - 1);
        equipment.EquippedWeapon = newWeapon;
        LogInventory($"{creature.Name} взял в руки {newWeapon.NameAccusative}.");
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
            var targetTile = currentTile.Level?.Map.GetTile(targetX, targetY);
            if (targetTile != null && targetTile.IsWalkable)
            {
                item.CurrentTile = targetTile;
                targetTile.ItemsOnTile.Add(item);
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
}