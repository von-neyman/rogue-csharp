using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток силы — перманентно увеличивает базовую силу.
/// </summary>
internal class ScrollOfStrength : Scroll
{
    internal ScrollOfStrength()
    {
        Name = "Свиток силы";
        NameAccusative = "Свиток силы";
        NameDative = "Свитку силы";
        Description = "Пергамент, исписанный рунами. При прочтении сила увеличивается навсегда.";
        ShortDescription = $"Увеличивает силу на {Scroll.StatIncrease}.";
        Symbol = '?';
    }

    internal override void Apply(Creature creature) => EffectSystem.IncreaseStrength(creature);
}