using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Systems;

/// <summary>
/// Боевая система. Рассчитывает попадание, урон и применяет особые способности существ и оружия.
/// </summary>
public static class CombatSystem
{
    private static readonly Random RandomGenerator = new();

    /// <summary>
    /// Провести атаку attacker по defender. Возвращает true, если атака попала.
    /// </summary>
    public static bool Attack(Creature attacker, Creature defender)
    {
        if (!CheckRecharge(attacker)) return false;
        bool hit = CheckFirstAttackEvasion(defender);
        if (hit) hit = RollHit(attacker, defender);
        if (!hit)
        {
            CheckCounterAttack(attacker, defender);
            hit = CheckSwiftStrike(attacker, defender);
        }
        if (hit) hit = CheckParry(attacker, defender);
        if (hit)
        {
            int damage = CalculateDamage(attacker);
            defender.TakeDamage(damage);
            CheckSpecialEffects(attacker, defender);
        }
        return hit;
    }

    /// <summary>Если атакующий должен перезаряжаться после атаки — пропускает следующий ход.
    /// Если атакующий уже перезаряжается - не может атаковать до следующего хода.</summary>
    private static bool CheckRecharge(Creature attacker)
    {
        if (attacker is IRecharge && attacker.SkipTurns > 0) return false;
        if (attacker is IRecharge) attacker.SkipTurns += 1;
        return true;
    }

    /// <summary>Вычислить шанс попадания и выполнить бросок.</summary>
    private static bool RollHit(Creature attacker, Creature defender)
    {
        int agilityMultiplier = GetAgilityMultiplier(attacker);
        int effectiveAgility = attacker.Agility * agilityMultiplier;
        int hitChance = effectiveAgility * 100 / (effectiveAgility + defender.Agility);
        return RandomGenerator.Next(100) < hitChance;
    }

    /// <summary>Вычислить модификатор ловкости.</summary>
    private static int GetAgilityMultiplier(Creature creature)
    {
        int agilityMultiplier = 1;
        if (creature is IEquipment equip && equip.EquippedWeapon != null)
        {
            agilityMultiplier = equip.EquippedWeapon switch
            {
                ILightWeapon w => w.AgilityMultiplier,
                IBalancedWeapon w => w.AgilityMultiplier,
                IHeavyWeapon w => w.AgilityMultiplier,
                _ => 1
            };
        }
        return agilityMultiplier;
    }

    /// <summary>Вычислить модификатор силы.</summary>
    private static int GetStrengthMultiplier(Creature creature)
    {
        int strengthMultiplier = 1;
        if (creature is IEquipment equip && equip.EquippedWeapon != null)
        {
            strengthMultiplier = equip.EquippedWeapon switch
            {
                ILightWeapon w => w.StrengthMultiplier,
                IBalancedWeapon w => w.StrengthMultiplier,
                IHeavyWeapon w => w.StrengthMultiplier,
                _ => 1
            };
        }
        return strengthMultiplier;
    }

    /// <summary>Проверка на уворот от первой атаки.</summary>
    private static bool CheckFirstAttackEvasion(Creature defender)
    {
        if (defender is IFirstAttackEvasion evasion && !evasion.HasEvaded)
        {
            evasion.HasEvaded = true;
            return false;
        }
        return true;
    }

    /// <summary>Проверка на контратаку защитника.</summary>
    private static void CheckCounterAttack(Creature attacker, Creature defender)
    {
        if (defender is IEquipment defenderEquip && defenderEquip.EquippedWeapon is ICounterattack)
            Attack(defender, attacker);
    }

    /// <summary>Проверка на стремительную атаку: второй шанс при промахе.</summary>
    private static bool CheckSwiftStrike(Creature attacker, Creature defender)
    {
        if (attacker is IEquipment attackerEquip && attackerEquip.EquippedWeapon is ISwiftStrike)
        {
            bool secondHit = RollHit(attacker, defender);
            if (!secondHit) CheckCounterAttack(attacker, defender);
            return secondHit;
        }
        return false;
    }

    /// <summary>Проверка парирования: 50% шанс отбить атаку.</summary>
    private static bool CheckParry(Creature attacker, Creature defender)
    {
        if (defender is IEquipment defEquip && defEquip.EquippedWeapon is IParry)
        {
            if (RandomGenerator.Next(2) == 0)
            {
                CheckCounterAttack(attacker, defender);
                return false;
            }
        }
        return true;
    }

    /// <summary>Рассчитать урон.</summary>
    private static int CalculateDamage(Creature attacker)
    {
        int strengthMultiplier = GetStrengthMultiplier(attacker);
        int damage = attacker.Strength * strengthMultiplier;
        if (attacker is IEquipment attackerEquip && attackerEquip.EquippedWeapon is ICrit && RandomGenerator.Next(2) == 0) damage *= 2;
        return damage;
    }

    /// <summary>Проверка на особые эффекты атаки.</summary>
    private static void CheckSpecialEffects(Creature attacker, Creature defender)
    {
        if (attacker is IReducesMaxHealth)
        {
            defender.BaseMaxHealth -= 1;
            if (defender.HealthBoostTurns > 0) defender.MaxHealth = defender.BaseMaxHealth * 2;
            else defender.MaxHealth = defender.BaseMaxHealth;
            if (defender.Health > defender.MaxHealth) defender.Health = defender.MaxHealth;
        }
        if (attacker is ISleepInducer && RandomGenerator.Next(2) == 0) defender.SkipTurns += 1;
    }
}