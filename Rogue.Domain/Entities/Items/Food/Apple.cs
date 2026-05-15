namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Яблоко — восстанавливает 30% от максимального здоровья.
/// </summary>
internal class Apple : Food
{
    internal override int HealingPercent => 30;

    internal Apple()
    {
        Name = "Яблоко";
        NameAccusative = "Яблоко";
        NameDative = "Яблоку";
        Description = "Свежее, будто только с ветки. Как оно здесь оказалось?";
        ShortDescription = $"Восстанавливает {HealingPercent} процентов здоровья.";
        Symbol = '%';
    }
}