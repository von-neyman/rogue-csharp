using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток силы — перманентно увеличивает базовую силу.
/// </summary>
public class ScrollOfStrength : Scroll
{
    public ScrollOfStrength()
    {
        Name = "Свиток силы";
        Description = "Пергамент, исписанный рунами. При прочтении сила увеличивается навсегда.";
        Symbol = '?';
    }

    public override void Apply(Creature creature) => EffectSystem.IncreaseStrength(creature);
}