using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Палаш — сбалансированное оружие. Механики: крит, парирование.
/// </summary>
public class Broadsword : Weapon, IBalancedWeapon, ICrit, IParry
{
    public Broadsword()
    {
        Name = "Палаш";
        Description = "Прямой широкий клинок. Один хорошо поставленный рубящий удар — " +
                      "под идеальным углом он разрубает плоть до кости. Прочная сталь " +
                      "позволяет отводить вражеские атаки в сторону.";
        Symbol = ')';
    }
}