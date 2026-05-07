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
        BaseStrength = 4;
        BaseAgility = 6;
        BaseMaxHealth = 30;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 6;
        Cost = 4;
        TreasureLoot = new GoldGoblet { IsOnGround = false };
        Symbol = 'V';
    }
}