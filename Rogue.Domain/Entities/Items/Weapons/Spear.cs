using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Копьё — тяжёлое оружие. Механики: контратака, парирование.
/// </summary>
public class Spear : Weapon, IHeavyWeapon, ICounterattack, IParry
{
    public Spear()
    {
        Name = "Копьё";
        Description = "Длинное древко с широким наконечником. Один колющий удар. " +
                      "Древко отбивает половину вражеских атак, наконечник карает за промахи. " +
                      "Держит дистанцию, прощает ошибки.";
        Symbol = ')';
    }
}