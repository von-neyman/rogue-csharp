using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Сабля — лёгкое оружие. Механики: крит, парирование.
/// </summary>
internal class Saber : Weapon, ILightWeapon, ICrit, IParry
{
    internal Saber()
    {
        Name = "Сабля";
        Description = "Изогнутый клинок с односторонней заточкой. Быстрый рубящий удар — " +
                      "если лезвие находит уязвимую точку, рана глубока и опасна. " +
                      "Гарда надёжно отбивает половину вражеских атак.";
        ShortDescription = "Сила ×2, Ловкость ×4, Крит, Парирование.";
        Symbol = ')';
    }
}