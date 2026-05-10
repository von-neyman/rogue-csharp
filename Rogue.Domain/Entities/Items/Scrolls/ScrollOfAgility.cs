using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

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

    public override void Apply(Creature creature) => EffectSystem.IncreaseAgility(creature);
}