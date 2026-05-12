using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток ловкости — перманентно увеличивает базовую ловкость.
/// </summary>
internal class ScrollOfAgility : Scroll
{
    internal ScrollOfAgility()
    {
        Name = "Свиток ловкости";
        Description = "Пергамент с лёгкими, струящимися письменами. Ловкость возрастает навсегда.";
        ShortDescription = $"Увеличивает ловкость на {Scroll.StatIncrease}.";
        Symbol = '?';
    }

    internal override void Apply(Creature creature) => EffectSystem.IncreaseAgility(creature);
}