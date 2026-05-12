using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток здоровья — перманентно увеличивает базовое максимальное здоровье.
/// </summary>
internal class ScrollOfHealth : Scroll
{
    internal ScrollOfHealth()
    {
        Name = "Свиток здоровья";
        Description = "Пергамент, от которого веет теплом. Здоровье увеличивается навсегда.";
        ShortDescription = $"Увеличивает здоровье на {Scroll.HealthIncrease}.";
        Symbol = '?';
    }

    internal override void Apply(Creature creature) => EffectSystem.IncreaseMaxHealth(creature);
}