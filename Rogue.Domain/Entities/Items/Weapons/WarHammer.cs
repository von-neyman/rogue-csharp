using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Боевой молот — тяжёлое оружие. Механики: крит, контратака.
/// </summary>
internal class WarHammer : Weapon, IHeavyWeapon, ICrit, ICounterattack
{
    internal WarHammer()
    {
        Name = "Боевой молот";
        Description = "Короткая рукоять, массивный боёк. Один оглушающий удар — и если молот " +
                      "попадает особенно удачно, враг уже не встаёт. Враг промахивается — " +
                      "молот возвращается быстрее, чем можно ожидать от такой тяжести.";
        Symbol = ')';
    }
}