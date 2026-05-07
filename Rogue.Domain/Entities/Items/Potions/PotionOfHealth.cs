using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Зелье здоровья — временно удваивает максимальное здоровье.
/// </summary>
public class PotionOfHealth : Potion
{
    public PotionOfHealth()
    {
        Name = "Зелье здоровья";
        Description = "Густая алая жидкость. Временно удваивает максимальное здоровье.";
        Symbol = '!';
    }

    public override void Apply(Creature creature)
    {
        creature.HealthBoostTurns += Duration;
        creature.MaxHealth = creature.BaseMaxHealth * 2;
        creature.Heal(creature.BaseMaxHealth);
    }
}