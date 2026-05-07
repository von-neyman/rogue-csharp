using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Зомби — неповоротливый, но живучий противник.
/// Характеристики: сила 4, ловкость 3, здоровье 30, враждебность 4.
/// </summary>
public class Zombie : Monster, IRandomWalk
{
    public Zombie()
    {
        BaseStrength = 4;
        BaseAgility = 3;
        BaseMaxHealth = 30;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 4;
        Cost = 1;
        TreasureLoot = new GoldSpoon { IsOnGround = false };
        Symbol = 'Z';
    }
}