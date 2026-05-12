using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Эсток — сбалансированное оружие. Механики: стремительная атака, парирование.
/// </summary>
internal class Estoc : Weapon, IBalancedWeapon, ISwiftStrike, IParry
{
    internal Estoc()
    {
        Name = "Эсток";
        Description = "Гранёный меч без лезвия — только остриё. Два выпада подряд: " +
                      "если первый не достиг цели, второй идёт немедленно. Прочная сталь " +
                      "позволяет встречать вражеский клинок и отводить его в сторону.";
        Symbol = ')';
    }
}