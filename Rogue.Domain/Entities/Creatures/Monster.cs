using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;
using Rogue.Domain.Systems;
using Rogue.Domain.World;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Базовый класс для всех монстров.
/// </summary>
public abstract class Monster : Creature, IHostility, ILoot, ICost, ICanMove, ICanAttack
{
    /// <summary>Радиус преследования: расстояние в клетках, с которого монстр начинает преследование.</summary>
    public int Hostility { get; set; }

    /// <summary>Сокровище, выпадающее при смерти.</summary>
    public Treasure? TreasureLoot { get; set; }

    /// <summary>Стоимость в очках пула монстров.</summary>
    public int Cost { get; set; }

    /// <summary>Атаковать другое существо.</summary>
    public bool Attack(Creature target)
    {
        return CombatSystem.Attack(this, target);
    }

    /// <summary>Совершить движение в указанном направлении.</summary>
    public bool Move(Direction direction, Level level)
    {
        return MovementSystem.PerformAction(this, direction, level);
    }
}