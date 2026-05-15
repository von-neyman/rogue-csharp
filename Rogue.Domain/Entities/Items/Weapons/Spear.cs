using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Копьё — тяжёлое оружие. Механики: контратака, парирование.
/// </summary>
internal class Spear : Weapon, IHeavyWeapon, ICounterattack, IParry
{
    internal Spear()
    {
        Name = "Копьё";
        NameAccusative = "Копьё";
        NameDative = "Копью";
        Description = "Длинное древко с широким наконечником. Один колющий удар. " +
                      "Древко отбивает половину вражеских атак, наконечник карает за промахи. " +
                      "Держит дистанцию, прощает ошибки.";
        ShortDescription = "Сила ×4, Ловкость ×1, Контратака, Парирование.";
        Symbol = ')';
    }
}