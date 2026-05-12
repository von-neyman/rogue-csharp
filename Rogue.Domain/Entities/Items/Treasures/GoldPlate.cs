namespace Rogue.Domain.Entities.Items.Treasures;

/// <summary>
/// Золотая тарелка — среднее сокровище. Ценность: 2.
/// Выпадает с привидений и огров.
/// </summary>
internal class GoldPlate : Treasure
{
    internal override int Value => 2;

    internal GoldPlate()
    {
        Name = "Золотая тарелка";
        Description = "Тяжёлая золотая тарелка. Стоит примерно 2 золотых.";
        Symbol = '$';
    }
}