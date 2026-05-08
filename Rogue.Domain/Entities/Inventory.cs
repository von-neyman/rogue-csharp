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
public class Inventory
{
    /// <summary>Список еды (до 9 слотов).</summary>
    public List<Food> Foods { get; set; } = [];

    /// <summary>Список оружия (до 9 слотов).</summary>
    public List<Weapon> Weapons { get; set; } = [];

    /// <summary>Список свитков (до 9 слотов).</summary>
    public List<Scroll> Scrolls { get; set; } = [];

    /// <summary>Список зелий (до 9 слотов).</summary>
    public List<Potion> Potions { get; set; } = [];

    /// <summary>Список сокровищ (без лимита).</summary>
    public List<Treasure> Treasures { get; set; } = [];

    /// <summary>Максимальное количество слотов для каждого типа (кроме сокровищ).</summary>
    public const int MaxSlotsPerType = 9;

    // Паттерн Facade: скрывает работу с пятью списками за простыми методами AddFood, AddWeapon, AddScroll, AddPotion, AddTreasure.

    /// <summary>Добавить еду в инвентарь. Убирает предмет с карты. Возвращает false, если нет места.</summary>
    public bool AddFood(Food food)
    {
        if (Foods.Count >= MaxSlotsPerType) return false;
        RemoveFromGround(food);
        Foods.Add(food);
        return true;
    }

    /// <summary>Добавить оружие в инвентарь. Убирает предмет с карты. Возвращает false, если нет места.</summary>
    public bool AddWeapon(Weapon weapon)
    {
        if (Weapons.Count >= MaxSlotsPerType) return false;
        RemoveFromGround(weapon);
        Weapons.Add(weapon);
        return true;
    }

    /// <summary>Добавить свиток в инвентарь. Убирает предмет с карты. Возвращает false, если нет места.</summary>
    public bool AddScroll(Scroll scroll)
    {
        if (Scrolls.Count >= MaxSlotsPerType) return false;
        RemoveFromGround(scroll);
        Scrolls.Add(scroll);
        return true;
    }

    /// <summary>Добавить зелье в инвентарь. Убирает предмет с карты. Возвращает false, если нет места.</summary>
    public bool AddPotion(Potion potion)
    {
        if (Potions.Count >= MaxSlotsPerType) return false;
        RemoveFromGround(potion);
        Potions.Add(potion);
        return true;
    }

    /// <summary>Добавить сокровище в инвентарь. Убирает предмет с карты. Всегда успешно.</summary>
    public void AddTreasure(Treasure treasure)
    {
        RemoveFromGround(treasure);
        Treasures.Add(treasure);
    }

    /// <summary>Общая ценность всех сокровищ в инвентаре.</summary>
    public int TotalTreasureValue => Treasures.Sum(t => t.Value);

    /// <summary>Убрать предмет с карты: удалить из списка предметов на тайле и обнулить CurrentTile.</summary>
    private static void RemoveFromGround(Item item)
    {
        item.CurrentTile?.ItemsOnTile.Remove(item);
        item.CurrentTile = null;
    }
}