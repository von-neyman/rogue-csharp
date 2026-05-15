namespace Rogue.Domain.Entities.Items.Treasures;

/// <summary>
/// Золотой кубок — самое дорогое сокровище. Ценность: 4.
/// Выпадает с вампиров и змеев-магов.
/// </summary>
internal class GoldGoblet : Treasure
{
    internal override int Value => 4;

    internal GoldGoblet()
    {
        Name = "Золотой кубок";
        NameAccusative = "Золотой кубок";
        NameDative = "Золотому кубку";
        Description = "Кубок, достойный короля.";
        ShortDescription = $"Стоит {Value} золотых.";
        Symbol = '$';
    }
}