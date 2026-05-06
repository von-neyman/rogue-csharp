using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Привидение — невидимый телепортирующийся противник.
/// Характеристики: сила 3, ловкость 6, здоровье 15, враждебность 3.
/// </summary>
public class Ghost : Monster, ITeleport, IInvisible
{
    public bool IsInvisible { get; set; } = true;

    public Ghost()
    {
        Strength = 3;
        Agility = 6;
        MaxHealth = 15;
        Health = 15;
        Hostility = 3;
        Cost = 2;
        TreasureLoot = new GoldPlate { IsOnGround = false };
        Symbol = 'G';
    }
}