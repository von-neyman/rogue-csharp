using Rogue.Domain.Common;

namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо способно перемещаться.</summary>
internal interface ICanMove
{
    bool Move(GameAction gameAction);
}

/// <summary>Случайное блуждание по комнате.</summary>
internal interface IRandomWalk { }

/// <summary>Телепортация в случайную точку комнаты.</summary>
internal interface ITeleport { }

/// <summary>Движение только по диагонали.</summary>
internal interface IDiagonalWalk { }

/// <summary>Движение на 2 клетки за ход ортогонально.</summary>
internal interface IDoubleStepWalk { }