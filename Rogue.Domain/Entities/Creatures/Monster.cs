using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Базовый класс для всех противников.
/// Содержит радиус преследования, добычу и абстрактный метод IdleMove.
/// </summary>
public abstract class Monster : Creature
{
    /// <summary>Радиус преследования: расстояние в клетках, с которого монстр начинает преследование.</summary>
    public int Hostility { get; set; }

    /// <summary>Сокровище, выпадающее при смерти.</summary>
    public Treasure? TreasureLoot { get; set; }

    /// <summary>Уникальный паттерн движения вне боя (idle).</summary>
    public abstract void IdleMove();
}