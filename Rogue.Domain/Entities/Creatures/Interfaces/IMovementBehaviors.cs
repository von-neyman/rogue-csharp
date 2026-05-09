using Rogue.Domain.Common;

namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо способно перемещаться.</summary>
public interface ICanMove
{
    bool Move(Direction direction);
}

/// <summary>Случайное блуждание по комнате.</summary>
public interface IRandomWalk { }

/// <summary>Телепортация в случайную точку комнаты.</summary>
public interface ITeleport { }

/// <summary>Движение только по диагонали.</summary>
public interface IDiagonalWalk { }

/// <summary>Движение на 2 клетки за ход ортогонально.</summary>
public interface IDoubleStepWalk { }