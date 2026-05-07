using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Змей-Маг — стремительный противник с гипнотическим укусом.
/// Характеристики: сила 4, ловкость 8, здоровье 20, враждебность 6.
/// Ходит только по диагонали. Атака может усыпить цель (50%).
/// </summary>
public class SnakeMage : Monster, IDiagonalWalk, ISleepInducer
{
    public SnakeMage()
    {
        BaseStrength = 4;
        BaseAgility = 8;
        BaseMaxHealth = 20;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 6;
        Cost = 4;
        TreasureLoot = new GoldGoblet { IsOnGround = false };
        Symbol = 'S';
    }
}