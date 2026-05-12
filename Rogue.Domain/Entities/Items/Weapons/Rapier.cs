using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Рапира — лёгкое оружие. Механики: стремительная атака, контратака.
/// </summary>
internal class Rapier : Weapon, ILightWeapon, ISwiftStrike, ICounterattack
{
    internal Rapier()
    {
        Name = "Рапира";
        Description = "Тонкий длинный клинок с изящной гардой. Мгновенный колющий выпад — " +
                      "и если остриё проходит мимо, рапира возвращается и колет снова. " +
                      "Враг промахивается? Рапира не прощает ошибок: контратака следует немедленно.";
        Symbol = ')';
    }
}