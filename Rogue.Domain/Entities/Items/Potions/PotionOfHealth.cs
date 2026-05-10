using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Зелье здоровья — временно удваивает максимальное здоровье.
/// </summary>
public class PotionOfHealth : Potion
{
    public PotionOfHealth()
    {
        Name = "Зелье здоровья";
        Description = "Густая алая жидкость. Временно удваивает максимальное здоровье.";
        Symbol = '!';
    }

    public override void Apply(Creature creature) => EffectSystem.BoostMaxHealth(creature);
}