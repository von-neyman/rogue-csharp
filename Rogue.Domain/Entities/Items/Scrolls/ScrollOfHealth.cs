using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток здоровья — перманентно увеличивает базовое максимальное здоровье.
/// </summary>
public class ScrollOfHealth : Scroll
{
    public ScrollOfHealth()
    {
        Name = "Свиток здоровья";
        Description = "Пергамент, от которого веет теплом. Здоровье увеличивается навсегда.";
        Symbol = '?';
    }

    public override void Apply(Creature creature)
    {
        creature.BaseMaxHealth += HealthIncrease;
        if (creature.HealthBoostTurns > 0)
        {
            creature.MaxHealth = creature.BaseMaxHealth * 2;
            creature.Heal(HealthIncrease * 2);
        }
        else
        {
            creature.MaxHealth = creature.BaseMaxHealth;
            creature.Heal(HealthIncrease);
        }
    }
}