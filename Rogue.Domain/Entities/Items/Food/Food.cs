namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Базовый класс для еды.
/// Восстанавливает процент от максимального здоровья игрока.
/// </summary>
public abstract class Food : Item
{
    /// <summary>Процент восстановления от максимального здоровья (20-80).</summary>
    public abstract int HealingPercent { get; }

    /// <summary>Рассчитать количество восстанавливаемого здоровья.</summary>
    public int CalculateHealing(int maxHealth) => maxHealth * HealingPercent / 100;
}