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
    public List<Food> Foods { get; set; } = new();

    /// <summary>Список оружия (до 9 слотов).</summary>
    public List<Weapon> Weapons { get; set; } = new();

    /// <summary>Список свитков (до 9 слотов).</summary>
    public List<Scroll> Scrolls { get; set; } = new();

    /// <summary>Список зелий (до 9 слотов).</summary>
    public List<Potion> Potions { get; set; } = new();

    /// <summary>Список сокровищ (без лимита).</summary>
    public List<Treasure> Treasures { get; set; } = new();

    /// <summary>Максимальное количество слотов для каждого типа (кроме сокровищ).</summary>
    public const int MaxSlotsPerType = 9;

    /// <summary>Добавить еду в инвентарь. Возвращает false, если нет места.</summary>
    public bool AddFood(Food food)
    {
        if (Foods.Count >= MaxSlotsPerType) return false;
        food.IsOnGround = false;
        Foods.Add(food);
        return true;
    }

    /// <summary>Добавить оружие в инвентарь. Возвращает false, если нет места.</summary>
    public bool AddWeapon(Weapon weapon)
    {
        if (Weapons.Count >= MaxSlotsPerType) return false;
        weapon.IsOnGround = false;
        Weapons.Add(weapon);
        return true;
    }

    /// <summary>Добавить свиток в инвентарь. Возвращает false, если нет места.</summary>
    public bool AddScroll(Scroll scroll)
    {
        if (Scrolls.Count >= MaxSlotsPerType) return false;
        scroll.IsOnGround = false;
        Scrolls.Add(scroll);
        return true;
    }

    /// <summary>Добавить зелье в инвентарь. Возвращает false, если нет места.</summary>
    public bool AddPotion(Potion potion)
    {
        if (Potions.Count >= MaxSlotsPerType) return false;
        potion.IsOnGround = false;
        Potions.Add(potion);
        return true;
    }

    /// <summary>Добавить сокровище в инвентарь. Всегда успешно.</summary>
    public void AddTreasure(Treasure treasure)
    {
        treasure.IsOnGround = false;
        Treasures.Add(treasure);
    }

    /// <summary>Общая ценность всех сокровищ в инвентаре.</summary>
    public int TotalTreasureValue => Treasures.Sum(t => t.Value);
}