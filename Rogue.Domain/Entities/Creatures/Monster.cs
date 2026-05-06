using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Базовый класс для всех монстров.
/// </summary>
public abstract class Monster : Creature, IHostility, ILoot, ICost
{
    /// <summary>Радиус преследования: расстояние в клетках, с которого монстр начинает преследование.</summary>
    public int Hostility { get; set; }

    /// <summary>Сокровище, выпадающее при смерти.</summary>
    public Treasure? TreasureLoot { get; set; }

    /// <summary>Стоимость в очках пула монстров.</summary>
    public int Cost { get; set; }
}