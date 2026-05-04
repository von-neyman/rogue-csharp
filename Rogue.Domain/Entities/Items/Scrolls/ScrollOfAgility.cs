using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток ловкости — перманентно увеличивает ловкость на 1.
/// </summary>
public class ScrollOfAgility : Scroll
{
    public ScrollOfAgility()
    {
        Name = "Свиток ловкости";
        Description = "Пергамент с лёгкими, струящимися письменами. Ловкость возрастает навсегда.";
        Symbol = '?';
    }

    public override void Apply(Hero player)
    {
        player.Agility += 1;
    }
}