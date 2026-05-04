using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Огр — медленный, но чрезвычайно сильный противник.
/// Характеристики: сила 8, ловкость 3, здоровье 40, враждебность 4.
/// Ходит на 2 клетки за ход. Атакует каждый второй ход (удар — отдых — удар).
/// </summary>
public class Ogre : Monster
{
    /// <summary>Отдыхает ли огр после предыдущей атаки.</summary>
    public bool IsResting { get; set; }

    public Ogre()
    {
        Strength = 8;
        Agility = 3;
        MaxHealth = 40;
        Health = 40;
        Hostility = 4;
        TreasureLoot = new GoldPlate { IsOnGround = false };
        Symbol = 'O';
    }

    public override void IdleMove()
    {
        // TODO: движение на 2 клетки за ход
    }
}