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
        // Если атакующий перезаряжается — пропускает ход, сбрасываем флаг
        if (attacker is IRecharge recharge && recharge.IsRecharging)
        {
            recharge.IsRecharging = false;
            return false;
        }
        // Вычисляем модификатор ловкости от оружия (1 если нет оружия)
        int agilityMultiplier = 1;
        if (attacker is IEquipment equip && equip.EquippedWeapon != null)
        {
            agilityMultiplier = equip.EquippedWeapon switch
            {
                ILightWeapon w => w.AgilityMultiplier,
                IBalancedWeapon w => w.AgilityMultiplier,
                IHeavyWeapon w => w.AgilityMultiplier,
                _ => 1
            };
        }
        // Проверка попадания
        int effectiveAgility = attacker.Agility * agilityMultiplier;
        int hitChance = effectiveAgility * 100 / (effectiveAgility + defender.Agility);
        bool hit = RandomGenerator.Next(100) < hitChance;
        // После атаки — перезарядка
        if (attacker is IRecharge attackerRecharge) attackerRecharge.IsRecharging = true;
        // Уворот: первая атака всегда промах
        if (defender is IFirstAttackEvasion evasion && !evasion.HasEvaded)
        {
            evasion.HasEvaded = true;
            hit = false;
        }
        // Если промах
        if (!hit)
        {
            // Контратака защитника
            if (defender is IEquipment defenderEquip && defenderEquip.EquippedWeapon is ICounterattack)
                Attack(defender, attacker);
            // Стремительная атака: второй шанс
            if (attacker is IEquipment attackerEquip && attackerEquip.EquippedWeapon is ISwiftStrike)
            {
                hit = RandomGenerator.Next(100) < hitChance;
                if (!hit)
                {
                    if (defender is IEquipment defEquip2 && defEquip2.EquippedWeapon is ICounterattack)
                        Attack(defender, attacker);
                    return false;
                }
            }
            else return false;
        }
        // Попадание — проверка парирования
        if (defender is IEquipment defEquipParry && defEquipParry.EquippedWeapon is IParry)
        {
            if (RandomGenerator.Next(2) == 0)
            {
                if (defender is IEquipment defEquipCounter && defEquipCounter.EquippedWeapon is ICounterattack)
                    Attack(defender, attacker);
                return false;
            }
        }
        // Расчёт урона
        int strengthMultiplier = 1;
        if (attacker is IEquipment attackerWeapon && attackerWeapon.EquippedWeapon != null)
        {
            strengthMultiplier = attackerWeapon.EquippedWeapon switch
            {
                ILightWeapon w => w.StrengthMultiplier,
                IBalancedWeapon w => w.StrengthMultiplier,
                IHeavyWeapon w => w.StrengthMultiplier,
                _ => 1
            };
        }
        int damage = attacker.Strength * strengthMultiplier;
        // Крит
        if (attacker is IEquipment attackerCrit && attackerCrit.EquippedWeapon is ICrit)
            if (RandomGenerator.Next(2) == 0) damage *= 2;
        // Нанесение урона
        defender.TakeDamage(damage);
        // Особые эффекты атакующего
        if (attacker is IReducesMaxHealth) defender.MaxHealth -= 1;
        if (attacker is ISleepInducer && RandomGenerator.Next(2) == 0)
        {
            // TODO: EffectSystem — усыпление
        }
        return true;
    }
}