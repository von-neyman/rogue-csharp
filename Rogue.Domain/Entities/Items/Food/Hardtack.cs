namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Сухарь — восстанавливает 20% от максимального здоровья.
/// </summary>
public class Hardtack : Food
{
    public override int HealingPercent => 20;

    public Hardtack()
    {
        Name = "Сухарь";
        Description = "Чёрствый, но съедобный. В подземелье и этому рад.";
        Symbol = '%';
    }
}