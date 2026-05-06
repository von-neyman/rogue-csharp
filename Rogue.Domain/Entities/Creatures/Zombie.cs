using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Зомби — неповоротливый, но живучий противник.
/// Характеристики: сила 4, ловкость 3, здоровье 30, враждебность 4.
/// Особых способностей нет.
/// </summary>
public class Zombie : Monster, IRandomWalk
{
    public Zombie()
    {
        Strength = 4;
        Agility = 3;
        MaxHealth = 30;
        Health = 30;
        Hostility = 4;
        Cost = 1;
        TreasureLoot = new GoldSpoon { IsOnGround = false };
        Symbol = 'Z';
    }
}