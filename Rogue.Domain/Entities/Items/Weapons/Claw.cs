using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Коготь — лёгкое оружие. Механики: крит, контратака.
/// </summary>
internal class Claw : Weapon, ILightWeapon, ICrit, ICounterattack
{
    internal Claw()
    {
        Name = "Коготь";
        NameAccusative = "Коготь";
        NameDative = "Когтю";
        Description = "Изогнутые лезвия на тыльной стороне ладони. Один глубокий " +
                      "вспарывающий удар — и если лезвие задело уязвимую точку, рана " +
                      "оказывается намного страшнее. Враг промахнулся? Коготь возвращается.";
        ShortDescription = "Сила ×2, Ловкость ×4, Крит, Контратака.";
        Symbol = ')';
    }
}