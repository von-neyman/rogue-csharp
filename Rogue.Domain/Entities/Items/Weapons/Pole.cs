using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Шест — тяжёлое оружие. Механики: стремительная атака, парирование.
/// </summary>
internal class Pole : Weapon, IHeavyWeapon, ISwiftStrike, IParry
{
    internal Pole()
    {
        Name = "Шест";
        Description = "Длинная окованная палка. Удар одним концом, промах — удар другим. " +
                      "Древко подставляется под вражеский замах, отбивая половину атак. " +
                      "Никакой магии — только физика и рычаг.";
        Symbol = ')';
    }
}