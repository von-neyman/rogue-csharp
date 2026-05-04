using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Potions;

/// <summary>
/// Базовый класс для зелий.
/// При использовании временно удваивают одну из характеристик игрока.
/// </summary>
public abstract class Potion : Item
{
    /// <summary>Длительность эффекта в ходах.</summary>
    public int Duration => 100;

    /// <summary>Применить эффект зелья к игроку.</summary>
    public abstract void Apply(Hero player);
}