using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Вампир — ловкий и живучий хищник.
/// Первая атака по нему всегда промахивается.
/// При успешной атаке уменьшает максимальное здоровье навсегда.
/// </summary>
public class Vampire : Monster, IRandomWalk, IFirstAttackEvasion, IReducesMaxHealth
{
    /// <summary>Уклонился ли уже от первого удара в этом бою.</summary>
    public bool HasEvaded { get; set; }

    public Vampire()
    {
        Name = "Вампир";
        Description = "Ловкий и живучий хищник.";
        BaseStrength = 4;
        BaseAgility = 6;
        BaseMaxHealth = 30;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 6;
        Cost = 4;
        TreasureLoot = new GoldGoblet();
        Symbol = 'V';
    }
}