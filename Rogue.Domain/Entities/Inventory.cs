using Rogue.Domain.Entities.Items;
using Rogue.Domain.Entities.Items.Food;
using Rogue.Domain.Entities.Items.Potions;
using Rogue.Domain.Entities.Items.Scrolls;
using Rogue.Domain.Entities.Items.Treasures;
using Rogue.Domain.Entities.Items.Weapons;

namespace Rogue.Domain.Entities;

/// <summary>
/// Рюкзак игрока. Хранит предметы пяти типов.
/// Для еды, оружия, свитков и зелий — до 9 слотов каждого типа.
/// Сокровища копятся без лимита.
/// </summary>
internal class Inventory
{
    /// <summary>Список еды (до 9 слотов).</summary>
    internal List<Food> Foods { get; set; } = [];

    /// <summary>Список оружия (до 9 слотов).</summary>
    internal List<Weapon> Weapons { get; set; } = [];

    /// <summary>Список свитков (до 9 слотов).</summary>
    internal List<Scroll> Scrolls { get; set; } = [];

    /// <summary>Список зелий (до 9 слотов).</summary>
    internal List<Potion> Potions { get; set; } = [];

    /// <summary>Список сокровищ (без лимита).</summary>
    internal List<Treasure> Treasures { get; set; } = [];

    /// <summary>Максимальное количество слотов для каждого типа (кроме сокровищ).</summary>
    internal const int MaxSlotsPerType = 9;

    // Паттерн Facade: скрывает работу с пятью списками за простыми методами AddFood, AddWeapon, AddScroll, AddPotion, AddTreasure.

    /// <summary>Добавить еду в инвентарь. Убирает предмет с карты. Возвращает false, если нет места.</summary>
    internal bool AddFood(Food food)
    {
        if (Foods.Count >= MaxSlotsPerType) return false;
        RemoveFromGround(food);
        Foods.Add(food);
        return true;
    }

    /// <summary>Добавить оружие в инвентарь. Убирает предмет с карты. Возвращает false, если нет места.</summary>
    internal bool AddWeapon(Weapon weapon)
    {
        if (Weapons.Count >= MaxSlotsPerType) return false;
        RemoveFromGround(weapon);
        Weapons.Add(weapon);
        return true;
    }

    /// <summary>Добавить свиток в инвентарь. Убирает предмет с карты. Возвращает false, если нет места.</summary>
    internal bool AddScroll(Scroll scroll)
    {
        if (Scrolls.Count >= MaxSlotsPerType) return false;
        RemoveFromGround(scroll);
        Scrolls.Add(scroll);
        return true;
    }

    /// <summary>Добавить зелье в инвентарь. Убирает предмет с карты. Возвращает false, если нет места.</summary>
    internal bool AddPotion(Potion potion)
    {
        if (Potions.Count >= MaxSlotsPerType) return false;
        RemoveFromGround(potion);
        Potions.Add(potion);
        return true;
    }

    /// <summary>Добавить сокровище в инвентарь. Убирает предмет с карты. Всегда успешно.</summary>
    internal void AddTreasure(Treasure treasure)
    {
        RemoveFromGround(treasure);
        Treasures.Add(treasure);
    }

    /// <summary>Общая ценность всех сокровищ в инвентаре.</summary>
    internal int TotalTreasureValue => Treasures.Sum(t => t.Value);

    /// <summary>Убрать предмет с карты: удалить из списка предметов на тайле и обнулить CurrentTile.</summary>
    private static void RemoveFromGround(Item item)
    {
        item.CurrentTile?.ItemsOnTile.Remove(item);
        item.CurrentTile = null;
    }
}