using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Базовый класс для свитков.
/// При использовании навсегда повышают одну из характеристик игрока.
/// </summary>
public abstract class Scroll : Item
{
    /// <summary>Применить эффект свитка к игроку.</summary>
    public abstract void Apply(Hero player);
}