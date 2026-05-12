using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Тесак — сбалансированное оружие. Механики: крит, контратака.
/// </summary>
internal class Cleaver : Weapon, IBalancedWeapon, ICrit, ICounterattack
{
    internal Cleaver()
    {
        Name = "Тесак";
        Description = "Широкий тяжёлый нож для рубки мяса. Один размашистый удар — " +
                      "и если лезвие зашло под верным углом, рана получается чудовищной. " +
                      "Враг зазевался после промаха? Тесак возвращается обратным ходом.";
        ShortDescription = "Сила ×3, Ловкость ×2, Крит, Контратака.";
        Symbol = ')';
    }
}