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
        BaseStrength = 3;
        BaseAgility = 6;
        BaseMaxHealth = 15;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 3;
        Cost = 2;
        TreasureLoot = new GoldPlate { IsOnGround = false };
        Symbol = 'G';
    }
}