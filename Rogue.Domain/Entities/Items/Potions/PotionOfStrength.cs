using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Зелье силы — временно удваивает силу.
/// </summary>
internal class PotionOfStrength : Potion
{
    internal PotionOfStrength()
    {
        Name = "Зелье силы";
        NameAccusative = "Зелье силы";
        NameDative = "Зелью силы";
        Description = "Мутная жидкость, пахнущая железом. Временно удваивает силу.";
        ShortDescription = $"Удваивает силу на {Potion.Duration} ходов.";
        Symbol = '!';
    }

    internal override void Apply(Creature creature) => EffectSystem.BoostStrength(creature);
}