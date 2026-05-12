namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Сыр — восстанавливает 40% от максимального здоровья.
/// </summary>
internal class Cheese : Food
{
    internal override int HealingPercent => 40;

    internal Cheese()
    {
        Name = "Сыр";
        Description = "Кусок выдержанного сыра. Запах чувствуется за километр.";
        ShortDescription = $"Восстанавливает {HealingPercent} процентов здоровья.";
        Symbol = '%';
    }
}