namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Копчёное мясо — восстанавливает 60% от максимального здоровья.
/// </summary>
internal class SmokedMeat : Food
{
    internal override int HealingPercent => 60;

    internal SmokedMeat()
    {
        Name = "Копчёное мясо";
        Description = "Полоска копчёной говядины. Пахнет дымком и специями.";
        Symbol = '%';
    }
}