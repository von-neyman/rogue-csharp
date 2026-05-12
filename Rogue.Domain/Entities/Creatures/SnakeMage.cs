using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Змей-Маг — стремительный противник с гипнотическим укусом.
/// Ходит только по диагонали. Атака может усыпить цель (50%).
/// </summary>
internal class SnakeMage : Monster, IDiagonalWalk, ISleepInducer
{
    internal SnakeMage()
    {
        Name = "Змей-Маг";
        Description = "Стремительный противник с гипнотическим укусом.";
        ShortDescription = "Монстр.";
        BaseStrength = 4;
        BaseAgility = 8;
        BaseMaxHealth = 20;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 6;
        Cost = 4;
        TreasureLoot = new GoldGoblet();
        Symbol = 'S';
    }
}