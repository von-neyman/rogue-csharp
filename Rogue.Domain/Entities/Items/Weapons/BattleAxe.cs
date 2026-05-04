using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Секира — тяжёлое оружие. Механики: крит, парирование.
/// </summary>
public class BattleAxe : Weapon, IHeavyWeapon, ICrit, IParry
{
    public BattleAxe()
    {
        Name = "Секира";
        Description = "Широкое лезвие на длинной рукояти. Один разрушительный замах — " +
                      "если лезвие входит глубоко, рана смертельна. Древко принимает " +
                      "на себя половину вражеских ударов.";
        Symbol = ')';
    }
}