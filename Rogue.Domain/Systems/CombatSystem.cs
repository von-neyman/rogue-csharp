using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Systems;

/// <summary>
/// Боевая система. Рассчитывает попадание, урон и применяет особые способности существ и оружия.
/// </summary>
internal static class CombatSystem
{
    /// <summary>Событие: запись в лог боя.</summary>
    internal static event Action<string>? OnLogMessage;

    /// <summary>Событие: игрок попал по врагу.</summary>
    internal static event Action? OnHitLanded;

    /// <summary>Событие: враг попал по игроку.</summary>
    internal static event Action? OnHitReceived;

    /// <summary>Событие: игрок убил монстра.</summary>
    internal static event Action? OnMonsterDefeated;

    private static readonly Random RandomGenerator = new();

    /// <summary>
    /// Провести атаку attacker по defender. Возвращает true, если атака попала.
    /// </summary>
    internal static bool Attack(Creature attacker, Creature defender)
    {
        CheckRecharge(attacker);
        bool hit = CheckFirstAttackEvasion(defender);
        if (hit) hit = RollHit(attacker, defender);
        if (!hit)
        {
            LogAttack($"{attacker.Name} атаковал {defender.NameAccusative}, но промахнулся.");
            CheckCounterAttack(attacker, defender);
            hit = CheckSwiftStrike(attacker, defender);
        }
        if (hit) hit = CheckParry(attacker, defender);
        if (hit)
        {
            int damage = CalculateDamage(attacker);
            LogAttack($"{attacker.Name} ударил {defender.NameAccusative} и нанёс {damage} ед. урона.");
            defender.TakeDamage(damage);
            if (!defender.IsAlive) LogAttack($"{defender.Name} погибает.");
            UpdateStatistics(attacker, defender);
            CheckSpecialEffects(attacker, defender);
        }
        return hit;
    }

    /// <summary>Записать сообщение в лог.</summary>
    private static void LogAttack(string message) => OnLogMessage?.Invoke(message);

    /// <summary>Если атакующий должен отдыхать или перезаряжаться после атаки — пропускает следующий ход.</summary>
    private static void CheckRecharge(Creature attacker)
    {
        if (attacker is IRelax) attacker.SkipTurns++;
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
            LogAttack($"{defender.Name} уклонился от первой атаки!");
            return false;
        }
        return true;
    }

    /// <summary>Проверка на контратаку защитника.</summary>
    private static void CheckCounterAttack(Creature attacker, Creature defender)
    {
        if (defender.SkipTurns == 0 && defender is IEquipment defenderEquip && defenderEquip.EquippedWeapon is ICounterattack)
        {
            LogAttack($"{defender.Name} контратакует {attacker.NameAccusative}!");
            Attack(defender, attacker);
        }
    }

    /// <summary>Проверка на стремительную атаку: второй шанс при промахе.</summary>
    private static bool CheckSwiftStrike(Creature attacker, Creature defender)
    {
        if (attacker is IEquipment attackerEquip && attackerEquip.EquippedWeapon is ISwiftStrike)
        {
            bool secondHit = RollHit(attacker, defender);
            if (secondHit) LogAttack($"{attacker.Name} пытается нанести удар ещё раз!");
            else
            {
                LogAttack($"{attacker.Name} бьёт по {defender.NameDative} ещё раз, но снова промахивается!");
                CheckCounterAttack(attacker, defender);
            }
            return secondHit;
        }
        return false;
    }

    /// <summary>Проверка парирования: 50% шанс отбить атаку.</summary>
    private static bool CheckParry(Creature attacker, Creature defender)
    {
        if (defender.SkipTurns == 0 && defender is IEquipment defEquip && defEquip.EquippedWeapon is IParry)
        {
            if (RandomGenerator.Next(2) == 0)
            {
                LogAttack($"{defender.Name} парировал атаку {attacker.NameAccusative}!");
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
            EffectSystem.ApplyMaxHealthReduction(defender);
            LogAttack($"Максимальное здоровье {defender.NameAccusative} уменьшено.");
        }
        if (attacker is ISleepInducer && RandomGenerator.Next(2) == 0)
        {
            EffectSystem.ApplySleep(defender);
            LogAttack($"{defender.Name} усыплён.");
        }
    }

    /// <summary>Обновить статистику по результатам атаки.</summary>
    private static void UpdateStatistics(Creature attacker, Creature defender)
    {
        if (attacker is Hero)
        {
            OnHitLanded?.Invoke();
            if (!defender.IsAlive) OnMonsterDefeated?.Invoke();
        }
        if (defender is Hero) OnHitReceived?.Invoke();
    }
}