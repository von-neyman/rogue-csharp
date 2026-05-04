using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Свиток здоровья — перманентно увеличивает максимальное здоровье на 5.
/// Текущее здоровье также повышается.
/// </summary>
public class ScrollOfHealth : Scroll
{
    public ScrollOfHealth()
    {
        Name = "Свиток здоровья";
        Description = "Пергамент, от которого веет теплом. Здоровье увеличивается навсегда.";
        Symbol = '?';
    }

    public override void Apply(Hero player)
    {
        player.MaxHealth += 5;
        player.Health += 5;
    }
}