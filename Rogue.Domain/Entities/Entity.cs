using Rogue.Domain.World;

namespace Rogue.Domain.Entities;

/// <summary>
/// Базовый класс для всех сущностей.
/// </summary>
internal abstract class Entity
{
    /// <summary>Клетка, на которой находится сущность. null — не на карте (например, в инвентаре).</summary>
    internal Tile? CurrentTile { get; set; }

    /// <summary>Символ для отображения на карте.</summary>
    internal char Symbol { get; set; }

    /// <summary>Название сущности.</summary>
    internal string Name { get; set; } = string.Empty;

    /// <summary>Описание сущности.</summary>
    internal string Description { get; set; } = string.Empty;

    /// <summary>Короткое описание с характеристиками.</summary>
    internal string ShortDescription { get; set; } = string.Empty;
}