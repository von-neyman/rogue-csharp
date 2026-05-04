using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Парные кинжалы — лёгкое оружие. Механики: стремительная атака, крит.
/// </summary>
public class DaggerPair : Weapon, ILightWeapon, ISwiftStrike, ICrit
{
    public DaggerPair()
    {
        Name = "Парные кинжалы";
        Description = "Два коротких клинка в обеих руках. Первый кинжал летит в цель — " +
                      "если проходит мимо, второй бьёт немедленно. Любое попадание может " +
                      "стать критическим: кинжал находит уязвимую точку, и рана становится " +
                      "куда серьёзнее, чем можно ждать от такого маленького лезвия.";
        Symbol = ')';
    }
}