namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Горшочек жаркого — восстанавливает 80% от максимального здоровья.
/// </summary>
public class MeatStew : Food
{
    public override int HealingPercent => 80;

    public MeatStew()
    {
        Name = "Горшочек жаркого";
        Description = "Глиняный горшочек с горячим мясом и подливой. Откуда это здесь?";
        Symbol = '%';
    }
}