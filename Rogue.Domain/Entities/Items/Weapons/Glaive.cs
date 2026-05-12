using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Глефа — тяжёлое оружие. Механики: стремительная атака, контратака.
/// </summary>
internal class Glaive : Weapon, IHeavyWeapon, ISwiftStrike, ICounterattack
{
    internal Glaive()
    {
        Name = "Глефа";
        Description = "Длинное древко с широким изогнутым лезвием. Колющий выпад сменяется " +
                      "рубящим — и если первая атака не достигла цели, разворот древка даёт " +
                      "вторую попытку. Враг пошатнулся после промаха? Глефа тут же наказывает.";
        ShortDescription = "Сила ×4, Ловкость ×1, Стремительная атака, Контратака.";
        Symbol = ')';
    }
}