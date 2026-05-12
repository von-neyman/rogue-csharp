using Rogue.Domain.Common;

namespace Rogue.Domain.Interfaces;

/// <summary>
/// Источник ввода игровых команд. Реализуется Presentation-слоем.
/// </summary>
public interface IInputSource
{
    /// <summary>Ожидать и вернуть следующее действие игрока.</summary>
    GameAction GetAction();
}