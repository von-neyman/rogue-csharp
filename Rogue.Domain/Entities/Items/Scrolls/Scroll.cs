using Rogue.Domain.Entities.Creatures;

namespace Rogue.Domain.Entities.Items.Scrolls;

/// <summary>
/// Базовый класс для свитков.
/// При использовании навсегда повышают одну из характеристик существа.
/// </summary>
internal abstract class Scroll : Item
{
    /// <summary>Величина увеличения силы или ловкости.</summary>
    internal const int StatIncrease = 1;

    /// <summary>Величина увеличения максимального здоровья (StatIncrease × 5).</summary>
    internal const int HealthIncrease = StatIncrease * 5;

    /// <summary>Применить эффект свитка к существу.</summary>
    internal abstract void Apply(Creature creature);
}