using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Зомби — неповоротливый, но живучий противник.
/// Характеристики: сила 4, ловкость 3, здоровье 30, враждебность 4.
/// Особых способностей нет.
/// </summary>
public class Zombie : Monster
{
    public override int Cost => 1;

    public Zombie()
    {
        Strength = 4;
        Agility = 3;
        MaxHealth = 30;
        Health = 30;
        Hostility = 4;
        TreasureLoot = new GoldSpoon { IsOnGround = false };
        Symbol = 'Z';
    }

    public override void IdleMove()
    {
        // TODO: случайное блуждание
    }
}