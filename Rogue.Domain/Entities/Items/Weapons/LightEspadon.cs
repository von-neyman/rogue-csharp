using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Лёгкий эспадон — лёгкое оружие. Механики: стремительная атака, парирование.
/// </summary>
internal class LightEspadon : Weapon, ILightWeapon, ISwiftStrike, IParry
{
    internal LightEspadon()
    {
        Name = "Лёгкий эспадон";
        NameAccusative = "Лёгкий эспадон";
        NameDative = "Лёгкому эспадону";
        Description = "Длинный двуручный меч из тонкой, гибкой стали. Два стремительных укола " +
                      "подряд: первый — разведка, второй — добивание. Клинок настолько лёгок, " +
                      "что им можно встретить вражеский удар и отвести его в сторону.";
        ShortDescription = "Сила ×2, Ловкость ×4, Стремительная атака, Парирование.";
        Symbol = ')';
    }
}