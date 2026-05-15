namespace Rogue.Domain.Entities.Items.Treasures;

/// <summary>
/// Золотая ложка — самое дешёвое сокровище. Ценность: 1.
/// Выпадает с зомби.
/// </summary>
internal class GoldSpoon : Treasure
{
    internal override int Value => 1;

    internal GoldSpoon()
    {
        Name = "Золотая ложка";
        NameAccusative = "Золотую ложку";
        NameDative = "Золотой ложке";
        Description = "Столовый прибор из чистого золота.";
        ShortDescription = $"Стоит {Value} золотой.";
        Symbol = '$';
    }
}