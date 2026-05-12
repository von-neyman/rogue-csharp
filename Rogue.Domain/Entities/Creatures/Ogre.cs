using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Огр — медленный, но чрезвычайно сильный противник.
/// Ходит на 2 клетки за ход. После атаки отдыхает следующий ход.
/// </summary>
internal class Ogre : Monster, IDoubleStepWalk, IRecharge
{
    internal Ogre()
    {
        Name = "Огр";
        Description = "Медленный, но чрезвычайно сильный противник.";
        BaseStrength = 8;
        BaseAgility = 3;
        BaseMaxHealth = 40;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 4;
        Cost = 2;
        TreasureLoot = new GoldPlate();
        Symbol = 'O';
    }
}