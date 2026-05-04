using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Зелье здоровья — временно удваивает максимальное здоровье.
/// Текущее здоровье также повышается.
/// </summary>
public class PotionOfHealth : Potion
{
    public PotionOfHealth()
    {
        Name = "Зелье здоровья";
        Description = "Густая алая жидкость. Временно удваивает максимальное здоровье.";
        Symbol = '!';
    }

    public override void Apply(Hero player)
    {
        // TODO: EffectSystem — удвоение макс. здоровья на Duration ходов
    }
}