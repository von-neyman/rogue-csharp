using Rogue.Domain.World;

namespace Rogue.Domain.Entities;

/// <summary>
/// Базовый класс для всех сущностей.
/// </summary>
public abstract class Entity
{
    /// <summary>Клетка, на которой находится сущность. null — не на карте (например, в инвентаре).</summary>
    public Tile? CurrentTile { get; set; }

    /// <summary>Символ для отображения на карте.</summary>
    public char Symbol { get; set; }

    /// <summary>Название сущности.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Описание сущности.</summary>
    public string Description { get; set; } = string.Empty;
}