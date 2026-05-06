using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Огр — медленный, но чрезвычайно сильный противник.
/// Характеристики: сила 8, ловкость 3, здоровье 40, враждебность 4.
/// Ходит на 2 клетки за ход. После атаки отдыхает следующий ход.
/// </summary>
public class Ogre : Monster, IDoubleStepWalk, IRecharge
{
    public bool IsRecharging { get; set; }

    public Ogre()
    {
        Strength = 8;
        Agility = 3;
        MaxHealth = 40;
        Health = 40;
        Hostility = 4;
        Cost = 2;
        TreasureLoot = new GoldPlate { IsOnGround = false };
        Symbol = 'O';
    }
}