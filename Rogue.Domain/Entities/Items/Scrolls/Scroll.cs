using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Базовый класс для свитков.
/// При использовании навсегда повышают одну из характеристик существа.
/// </summary>
public abstract class Scroll : Item
{
    /// <summary>Величина увеличения силы или ловкости.</summary>
    public const int StatIncrease = 1;

    /// <summary>Величина увеличения максимального здоровья (StatIncrease × 5).</summary>
    public const int HealthIncrease = StatIncrease * 5;

    /// <summary>Применить эффект свитка к существу.</summary>
    public abstract void Apply(Creature creature);
}