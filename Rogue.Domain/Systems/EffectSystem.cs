using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Items.Potions;
using Rogue.Domain.Entities.Items.Scrolls;

namespace Rogue.Domain.Systems;

/// <summary>
/// Система эффектов: управление временными и постоянными изменениями характеристик существ.
/// </summary>
internal static class EffectSystem
{
    /// <summary>Обработка счётчиков эффектов существа.</summary>
    internal static void TickEffects(Creature creature)
    {
        if (creature.StrengthBoostTurns > 0)
        {
            creature.StrengthBoostTurns--;
            if (creature.StrengthBoostTurns == 0) creature.Strength = creature.BaseStrength;
        }
        if (creature.AgilityBoostTurns > 0)
        {
            creature.AgilityBoostTurns--;
            if (creature.AgilityBoostTurns == 0) creature.Agility = creature.BaseAgility;
        }
        if (creature.HealthBoostTurns > 0)
        {
            creature.HealthBoostTurns--;
            if (creature.HealthBoostTurns == 0)
            {
                creature.MaxHealth = creature.BaseMaxHealth;
                creature.Health -= creature.BaseMaxHealth;
                if (creature.Health <= 0) creature.Health = 1;
            }
        }
    }

    /// <summary>Временно удвоить силу на Duration ходов.</summary>
    internal static void BoostStrength(Creature creature)
    {
        creature.StrengthBoostTurns += Potion.Duration;
        creature.Strength = creature.BaseStrength * 2;
    }

    /// <summary>Временно удвоить ловкость на Duration ходов.</summary>
    internal static void BoostAgility(Creature creature)
    {
        creature.AgilityBoostTurns += Potion.Duration;
        creature.Agility = creature.BaseAgility * 2;
    }

    /// <summary>Временно удвоить максимальное здоровье на Duration ходов.</summary>
    internal static void BoostMaxHealth(Creature creature)
    {
        creature.HealthBoostTurns += Potion.Duration;
        creature.MaxHealth = creature.BaseMaxHealth * 2;
        creature.Heal(creature.BaseMaxHealth);
    }

    /// <summary>Перманентно увеличить базовую силу.</summary>
    internal static void IncreaseStrength(Creature creature)
    {
        creature.BaseStrength += Scroll.StatIncrease;
        creature.Strength = creature.StrengthBoostTurns > 0 ? creature.BaseStrength * 2 : creature.BaseStrength;
    }

    /// <summary>Перманентно увеличить базовую ловкость.</summary>
    internal static void IncreaseAgility(Creature creature)
    {
        creature.BaseAgility += Scroll.StatIncrease;
        creature.Agility = creature.AgilityBoostTurns > 0 ? creature.BaseAgility * 2 : creature.BaseAgility;
    }

    /// <summary>Перманентно увеличить базовое максимальное здоровье.</summary>
    internal static void IncreaseMaxHealth(Creature creature)
    {
        creature.BaseMaxHealth += Scroll.HealthIncrease;
        if (creature.HealthBoostTurns > 0)
        {
            creature.MaxHealth = creature.BaseMaxHealth * 2;
            creature.Heal(Scroll.HealthIncrease * 2);
        }
        else
        {
            creature.MaxHealth = creature.BaseMaxHealth;
            creature.Heal(Scroll.HealthIncrease);
        }
    }

    /// <summary>Усыпить существо: пропускает следующий ход.</summary>
    internal static void ApplySleep(Creature creature) => creature.SkipTurns += 1;

    /// <summary>Уменьшить базовое максимальное здоровье на 1.</summary>
    internal static void ApplyMaxHealthReduction(Creature creature)
    {
        creature.BaseMaxHealth -= 1;
        if (creature.HealthBoostTurns > 0) creature.MaxHealth = creature.BaseMaxHealth * 2;
        else creature.MaxHealth = creature.BaseMaxHealth;
        if (creature.Health > creature.MaxHealth) creature.Health = creature.MaxHealth;
    }
}