using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Зелье здоровья — временно удваивает максимальное здоровье.
/// </summary>
internal class PotionOfHealth : Potion
{
    internal PotionOfHealth()
    {
        Name = "Зелье здоровья";
        NameAccusative = "Зелье здоровья";
        NameDative = "Зелью здоровья";
        Description = "Густая алая жидкость. Временно удваивает максимальное здоровье.";
        ShortDescription = $"Удваивает здоровье на {Potion.Duration} ходов.";
        Symbol = '!';
    }

    internal override void Apply(Creature creature) => EffectSystem.BoostMaxHealth(creature);
}