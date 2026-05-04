using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Коготь — лёгкое оружие. Механики: крит, контратака.
/// </summary>
public class Claw : Weapon, ILightWeapon, ICrit, ICounterattack
{
    public Claw()
    {
        Name = "Коготь";
        Description = "Изогнутые лезвия на тыльной стороне ладони. Один глубокий " +
                      "вспарывающий удар — и если лезвие задело уязвимую точку, рана " +
                      "оказывается намного страшнее. Враг промахнулся? Коготь возвращается.";
        Symbol = ')';
    }
}