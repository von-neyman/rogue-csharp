using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Длинный меч — сбалансированное оружие. Механики: контратака, парирование.
/// </summary>
internal class Longsword : Weapon, IBalancedWeapon, ICounterattack, IParry
{
    internal Longsword()
    {
        Name = "Длинный меч";
        Description = "Классический рыцарский меч. Один мощный вертикальный удар сверху. " +
                      "Гарда ловит вражеский клинок и отводит его. Враг промахнулся — " +
                      "обратный замах не заставляет себя ждать.";
        Symbol = ')';
    }
}