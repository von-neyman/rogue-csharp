using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Секира — тяжёлое оружие. Механики: крит, парирование.
/// </summary>
internal class BattleAxe : Weapon, IHeavyWeapon, ICrit, IParry
{
    internal BattleAxe()
    {
        Name = "Секира";
        NameAccusative = "Секиру";
        NameDative = "Секире";
        Description = "Широкое лезвие на длинной рукояти. Один разрушительный замах — " +
                      "если лезвие входит глубоко, рана смертельна. Древко принимает " +
                      "на себя половину вражеских ударов.";
        ShortDescription = "Сила ×4, Ловкость ×1, Крит, Парирование.";
        Symbol = ')';
    }
}