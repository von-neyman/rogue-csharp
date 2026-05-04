namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Яблоко — восстанавливает 30% от максимального здоровья.
/// </summary>
public class Apple : Food
{
    public override int HealingPercent => 30;

    public Apple()
    {
        Name = "Яблоко";
        Description = "Свежее, будто только с ветки. Как оно здесь оказалось?";
        Symbol = '%';
    }
}