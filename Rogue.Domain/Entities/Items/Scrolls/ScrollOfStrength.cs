using Rogue.Domain.Entities.Creatures;

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

    public override void Apply(Creature creature)
    {
        creature.BaseStrength += StatIncrease;
        if (creature.StrengthBoostTurns > 0) creature.Strength = creature.BaseStrength * 2;
        else creature.Strength = creature.BaseStrength;
    }
}