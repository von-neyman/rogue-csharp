using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Огр — медленный, но чрезвычайно сильный противник.
/// Ходит на 2 клетки за ход. После атаки отдыхает следующий ход.
/// </summary>
internal class Ogre : Monster, IDoubleStepWalk, IRelax
{
    internal Ogre()
    {
        Name = "Огр";
        NameAccusative = "Огра";
        NameDative = "Огру";
        Description = "Медленный, но чрезвычайно сильный противник.";
        ShortDescription = "Монстр.";
        Faction = Faction.DungeonMonsters;
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