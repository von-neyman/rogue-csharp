using Rogue.Domain.Entities.Creatures;

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

    public override void Apply(Creature creature)
    {
        creature.AgilityBoostTurns += Duration;
        creature.Agility = creature.BaseAgility * 2;
    }
}