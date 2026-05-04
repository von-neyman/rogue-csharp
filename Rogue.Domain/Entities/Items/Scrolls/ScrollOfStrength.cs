using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток силы — перманентно увеличивает силу на 1.
/// </summary>
public class ScrollOfStrength : Scroll
{
    public ScrollOfStrength()
    {
        Name = "Свиток силы";
        Description = "Пергамент, исписанный рунами. При прочтении сила увеличивается навсегда.";
        Symbol = '?';
    }

    public override void Apply(Hero player)
    {
        player.Strength += 1;
    }
}