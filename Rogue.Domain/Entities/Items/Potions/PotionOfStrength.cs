using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Зелье силы — временно удваивает силу.
/// </summary>
public class PotionOfStrength : Potion
{
    public PotionOfStrength()
    {
        Name = "Зелье силы";
        Description = "Мутная жидкость, пахнущая железом. Временно удваивает силу.";
        Symbol = '!';
    }

    public override void Apply(Creature creature) => EffectSystem.BoostStrength(creature);
}