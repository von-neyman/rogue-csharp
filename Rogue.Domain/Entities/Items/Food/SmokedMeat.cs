namespace Rogue.Domain.Entities.Items.Food;

/// <summary>
/// Копчёное мясо — восстанавливает 60% от максимального здоровья.
/// </summary>
public class SmokedMeat : Food
{
    public override int HealingPercent => 60;

    public SmokedMeat()
    {
        Name = "Копчёное мясо";
        Description = "Полоска копчёной говядины. Пахнет дымком и специями.";
        Symbol = '%';
    }
}