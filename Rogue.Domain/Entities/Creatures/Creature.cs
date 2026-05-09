using Rogue.Domain.Entities.Creatures.Interfaces;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Базовый класс для всех живых существ.
/// </summary>
public abstract class Creature : Entity
{
    /// <summary>Текущее здоровье. При падении до 0 существо погибает.</summary>
    public int Health { get; set; }

    /// <summary>Максимальное здоровье с учётом временных эффектов.</summary>
    public int MaxHealth { get; set; }

    /// <summary>Сила с учётом временных эффектов.</summary>
    public int Strength { get; set; }

    /// <summary>Ловкость с учётом временных эффектов.</summary>
    public int Agility { get; set; }

    /// <summary>Базовая максимальное здоровье без временных эффектов.</summary>
    public int BaseMaxHealth { get; set; }

    /// <summary>Базовая сила без временных эффектов.</summary>
    public int BaseStrength { get; set; }

    /// <summary>Базовая ловкость без временных эффектов.</summary>
    public int BaseAgility { get; set; }

    /// <summary>Счётчик ходов до окончания удвоения силы. 0 — эффект не активен.</summary>
    public int StrengthBoostTurns { get; set; }

    /// <summary>Счётчик ходов до окончания удвоения ловкости. 0 — эффект не активен.</summary>
    public int AgilityBoostTurns { get; set; }

    /// <summary>Счётчик ходов до окончания удвоения максимального здоровья. 0 — эффект не активен.</summary>
    public int HealthBoostTurns { get; set; }

    /// <summary>Счётчик пропущенных ходов. Если больше 0, существо бездействует.</summary>
    public int SkipTurns { get; set; }

    /// <summary>Живо ли существо.</summary>
    public bool IsAlive { get; set; } = true;

    /// <summary>Получить урон. Здоровье не опускается ниже 0. Если здоровье упало до 0 — существо умирает.</summary>
    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Health = 0;
            IsAlive = false;
            if (this is ILoot loot && loot.TreasureLoot != null) loot.DropLoot();
        }
    }

    /// <summary>Восстановить здоровье. Не превышает максимальное здоровье.</summary>
    public void Heal(int amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }
}