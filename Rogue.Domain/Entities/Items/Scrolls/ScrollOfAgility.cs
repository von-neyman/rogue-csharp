using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток ловкости — перманентно увеличивает базовую ловкость.
/// </summary>
public class ScrollOfAgility : Scroll
{
    public ScrollOfAgility()
    {
        Name = "Свиток ловкости";
        Description = "Пергамент с лёгкими, струящимися письменами. Ловкость возрастает навсегда.";
        Symbol = '?';
    }

    public override void Apply(Creature creature)
    {
        creature.BaseAgility += StatIncrease;
        if (creature.AgilityBoostTurns > 0) creature.Agility = creature.BaseAgility * 2;
        else creature.Agility = creature.BaseAgility;
    }
}