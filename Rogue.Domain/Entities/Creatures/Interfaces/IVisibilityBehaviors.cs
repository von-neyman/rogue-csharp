namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо может быть невидимым.</summary>
internal interface IInvisible
{
    bool IsInvisible { get; set; }
}