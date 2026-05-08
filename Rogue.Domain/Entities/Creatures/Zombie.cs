using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Зомби — неповоротливый, но живучий противник.
/// </summary>
public class Zombie : Monster, IRandomWalk
{
    public Zombie()
    {
        Name = "Зомби";
        Description = "Неповоротливый, но живучий противник.";
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