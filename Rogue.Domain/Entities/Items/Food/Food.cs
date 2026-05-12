namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Базовый класс для еды.
/// Восстанавливает процент от максимального здоровья игрока.
/// </summary>
internal abstract class Food : Item
{
    /// <summary>Процент восстановления от максимального здоровья (20-80).</summary>
    internal abstract int HealingPercent { get; }

    /// <summary>Рассчитать количество восстанавливаемого здоровья.</summary>
    internal int CalculateHealing(int maxHealth) => maxHealth * HealingPercent / 100;
}