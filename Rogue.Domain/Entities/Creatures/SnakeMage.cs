using Rogue.Domain.Common;
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
        NameAccusative = "Змея-Мага";
        NameDative = "Змею-Магу";
        Description = "Стремительный противник с гипнотическим укусом.";
        ShortDescription = "Монстр.";
        Faction = Faction.DungeonMonsters;
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