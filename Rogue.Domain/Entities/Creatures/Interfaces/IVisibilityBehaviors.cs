namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо может быть невидимым.</summary>
public interface IInvisible
{
    bool IsInvisible { get; set; }
}