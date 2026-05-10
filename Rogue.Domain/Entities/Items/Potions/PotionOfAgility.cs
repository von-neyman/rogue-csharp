using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Зелье ловкости — временно удваивает ловкость.
/// </summary>
public class PotionOfAgility : Potion
{
    public PotionOfAgility()
    {
        Name = "Зелье ловкости";
        Description = "Искрящаяся жидкость, убегающая от стенок склянки. Временно удваивает ловкость.";
        Symbol = '!';
    }

    public override void Apply(Creature creature) => EffectSystem.BoostAgility(creature);
}