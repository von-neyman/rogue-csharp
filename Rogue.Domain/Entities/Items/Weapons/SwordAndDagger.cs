using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Шпага и кинжал — лёгкое оружие. Механики: контратака, парирование.
/// </summary>
internal class SwordAndDagger : Weapon, ILightWeapon, ICounterattack, IParry
{
    internal SwordAndDagger()
    {
        Name = "Шпага и кинжал";
        NameAccusative = "Шпагу и кинжал";
        NameDative = "Шпаге и кинжалу";
        Description = "Шпага в ведущей руке для уколов, кинжал во второй — для защиты. " +
                      "Точный выпад. Кинжал отбивает половину вражеских атак, шпага " +
                      "наказывает за промахи. В дуэли один на один владелец этой пары " +
                      "практически неуязвим.";
        ShortDescription = "Сила ×2, Ловкость ×4, Контратака, Парирование.";
        Symbol = ')';
    }
}