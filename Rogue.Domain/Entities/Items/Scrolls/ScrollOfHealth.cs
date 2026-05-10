using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

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

    public override void Apply(Creature creature) => EffectSystem.IncreaseMaxHealth(creature);
}