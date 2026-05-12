using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Зелье ловкости — временно удваивает ловкость.
/// </summary>
internal class PotionOfAgility : Potion
{
    internal PotionOfAgility()
    {
        Name = "Зелье ловкости";
        Description = "Искрящаяся жидкость, убегающая от стенок склянки. Временно удваивает ловкость.";
        ShortDescription = $"Удваивает ловкость на {Potion.Duration} ходов.";
        Symbol = '!';
    }

    internal override void Apply(Creature creature) => EffectSystem.BoostAgility(creature);
}