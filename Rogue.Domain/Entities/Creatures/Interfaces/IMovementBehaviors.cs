namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Случайное блуждание по комнате.</summary>
public interface IRandomWalk { }

/// <summary>Телепортация в случайную точку комнаты.</summary>
public interface ITeleport { }

/// <summary>Движение только по диагонали.</summary>
public interface IDiagonalWalk { }

/// <summary>Движение на 2 клетки за ход ортогонально.</summary>
public interface IDoubleStepWalk { }