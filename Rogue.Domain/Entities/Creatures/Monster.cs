using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;
using Rogue.Domain.Systems;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Базовый класс для всех монстров.
/// </summary>
public abstract class Monster : Creature, IHostility, ILoot, ICost, ICanMove, ICanAttack
{
    /// <summary>Радиус преследования.</summary>
    public int Hostility { get; set; }

    /// <summary>Сокровище, выпадающее при смерти.</summary>
    public Treasure? TreasureLoot { get; set; }

    /// <summary>Стоимость в очках пула монстров.</summary>
    public int Cost { get; set; }

    /// <summary>Атаковать другое существо.</summary>
    public bool Attack(Creature target) => CombatSystem.Attack(this, target);

    /// <summary>Совершить действие.</summary>
    public bool Move(GameAction gameAction) => MovementSystem.PerformAction(this, gameAction);

    /// <summary>Выбросить сокровище при смерти.</summary>
    public void DropLoot() => InventorySystem.DropLoot(this);
}