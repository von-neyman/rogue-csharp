using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Лёгкий эспадон — лёгкое оружие. Механики: стремительная атака, парирование.
/// </summary>
public class LightEspadon : Weapon, ILightWeapon, ISwiftStrike, IParry
{
    public LightEspadon()
    {
        Name = "Лёгкий эспадон";
        Description = "Длинный двуручный меч из тонкой, гибкой стали. Два стремительных укола " +
                      "подряд: первый — разведка, второй — добивание. Клинок настолько лёгок, " +
                      "что им можно встретить вражеский удар и отвести его в сторону.";
        Symbol = ')';
    }
}