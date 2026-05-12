using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Двуручный меч — тяжёлое оружие. Механики: стремительная атака, крит.
/// </summary>
internal class Greatsword : Weapon, IHeavyWeapon, ISwiftStrike, ICrit
{
    internal Greatsword()
    {
        Name = "Двуручный меч";
        Description = "Огромный клинок, который держат двумя руками. Замах — и лезвие " +
                      "обрушивается на врага. Промахнулся? Обратный ход меча даёт второй шанс. " +
                      "Попадание — почти гарантированная смерть. Крит — враг разваливается надвое.";
        Symbol = ')';
    }
}