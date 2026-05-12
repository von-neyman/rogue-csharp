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
        Description = "Кубок, достойный короля. Стоит примерно 4 золотых.";
        Symbol = '$';
    }
}