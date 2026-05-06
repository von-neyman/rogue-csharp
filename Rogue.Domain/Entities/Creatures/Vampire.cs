using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Вампир — ловкий и живучий хищник.
/// Характеристики: сила 4, ловкость 6, здоровье 30, враждебность 6.
/// Первая атака по нему всегда промахивается.
/// При успешной атаке уменьшает максимальное здоровье навсегда.
/// </summary>
public class Vampire : Monster, IRandomWalk, IFirstAttackEvasion, IReducesMaxHealth
{
    public bool HasEvaded { get; set; }

    public Vampire()
    {
        Strength = 4;
        Agility = 6;
        MaxHealth = 30;
        Health = 30;
        Hostility = 6;
        Cost = 4;
        TreasureLoot = new GoldGoblet { IsOnGround = false };
        Symbol = 'V';
    }
}