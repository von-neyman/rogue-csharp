namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Горшочек жаркого — восстанавливает 80% от максимального здоровья.
/// </summary>
internal class MeatStew : Food
{
    internal override int HealingPercent => 80;

    internal MeatStew()
    {
        Name = "Горшочек жаркого";
        Description = "Глиняный горшочек с горячим мясом и подливой. Откуда это здесь?";
        Symbol = '%';
    }
}