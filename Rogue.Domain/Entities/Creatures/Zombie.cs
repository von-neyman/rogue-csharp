using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Зомби — неповоротливый, но живучий противник.
/// </summary>
internal class Zombie : Monster, IRandomWalk
{
    internal Zombie()
    {
        Name = "Зомби";
        NameAccusative = "Зомби";
        NameDative = "Зомби";
        Description = "Неповоротливый, но живучий противник.";
        ShortDescription = "Монстр.";
        Faction = Faction.DungeonMonsters;
        BaseStrength = 4;
        BaseAgility = 3;
        BaseMaxHealth = 30;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 4;
        Cost = 1;
        TreasureLoot = new GoldSpoon();
        Symbol = 'Z';
    }
}