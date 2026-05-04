namespace Rogue.Domain.Entities.Items.Treasures;

/// <summary>
/// Золотая ложка — самое дешёвое сокровище. Ценность: 1.
/// Выпадает с зомби.
/// </summary>
public class GoldSpoon : Treasure
{
    public override int Value => 1;

    public GoldSpoon()
    {
        Name = "Золотая ложка";
        Description = "Столовый прибор из чистого золота. Стоит примерно 1 золотой.";
        Symbol = '$';
    }
}