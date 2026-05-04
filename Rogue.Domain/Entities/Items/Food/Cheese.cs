namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Сыр — восстанавливает 40% от максимального здоровья.
/// </summary>
public class Cheese : Food
{
    public override int HealingPercent => 40;

    public Cheese()
    {
        Name = "Сыр";
        Description = "Кусок выдержанного сыра. Запах чувствуется за километр.";
        Symbol = '%';
    }
}