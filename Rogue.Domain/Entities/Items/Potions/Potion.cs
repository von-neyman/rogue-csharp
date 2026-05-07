using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Базовый класс для зелий.
/// При использовании временно удваивают одну из характеристик.
/// </summary>
public abstract class Potion : Item
{
    /// <summary>Длительность эффекта в ходах.</summary>
    public const int Duration = 100;

    /// <summary>Применить эффект зелья к существу.</summary>
    public abstract void Apply(Creature creature);
}