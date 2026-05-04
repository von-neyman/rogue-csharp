using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Сабля — лёгкое оружие. Механики: крит, парирование.
/// </summary>
public class Saber : Weapon, ILightWeapon, ICrit, IParry
{
    public Saber()
    {
        Name = "Сабля";
        Description = "Изогнутый клинок с односторонней заточкой. Быстрый рубящий удар — " +
                      "если лезвие находит уязвимую точку, рана глубока и опасна. " +
                      "Гарда надёжно отбивает половину вражеских атак.";
        Symbol = ')';
    }
}