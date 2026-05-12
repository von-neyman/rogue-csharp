namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Сухарь — восстанавливает 20% от максимального здоровья.
/// </summary>
internal class Hardtack : Food
{
    internal override int HealingPercent => 20;

    internal Hardtack()
    {
        Name = "Сухарь";
        Description = "Чёрствый, но съедобный. В подземелье и этому рад.";
        ShortDescription = $"Восстанавливает {HealingPercent} процентов здоровья.";
        Symbol = '%';
    }
}