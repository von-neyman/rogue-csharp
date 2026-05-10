using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.World;

namespace Rogue.Domain.Systems;

/// <summary>
/// Система передвижения. Обрабатывает ход существ: движение, атаку, бездействие.
/// </summary>
public static class MovementSystem
{
    /// <summary>
    /// Выполнить ход существа. Возвращает true, если ход состоялся (движение/атака/бездействие).
    /// </summary>
    public static bool PerformAction(Creature creature, Direction direction)
    {
        if (CheckSkipTurns(creature)) return true;
        if (CheckDirectionNone(creature, direction)) return true;
        (int targetX, int targetY) = GetTargetPosition(creature, direction);
        if (targetX == -1) return false;
        var targetTile = creature.CurrentTile?.Level?.Map.GetTile(targetX, targetY);
        if (targetTile == null || !targetTile.IsWalkable) return false;
        var targetCreature = GetCreatureAt(targetTile);
        if (targetCreature != null && creature is ICanAttack attacker) attacker.Attack(targetCreature);
        else
        {
            MoveTo(creature, targetTile);
            if (creature is IInventory collector) collector.CollectItems();
        }
        EffectSystem.TickEffects(creature);
        return true;
    }

    /// <summary>Вычислить координаты клетки в указанном направлении от существа.</summary>
    private static (int X, int Y) GetTargetPosition(Creature creature, Direction direction)
    {
        if (creature.CurrentTile == null) return (-1, -1);
        int x = creature.CurrentTile.X;
        int y = creature.CurrentTile.Y;
        return direction switch
        {
            Direction.Up => (x, y - 1),
            Direction.Down => (x, y + 1),
            Direction.Left => (x - 1, y),
            Direction.Right => (x + 1, y),
            Direction.UpLeft => (x - 1, y - 1),
            Direction.UpRight => (x + 1, y - 1),
            Direction.DownLeft => (x - 1, y + 1),
            Direction.DownRight => (x + 1, y + 1),
            _ => (x, y)
        };
    }

    /// <summary>Найти первое живое существо на клетке.</summary>
    private static Creature? GetCreatureAt(Tile tile)
    {
        return tile.CreaturesOnTile.FirstOrDefault(c => c.IsAlive);
    }

    /// <summary>Вернуть true если creature пропускает ход.</summary>
    private static bool CheckSkipTurns(Creature creature)
    {
        if (creature.SkipTurns > 0)
        {
            creature.SkipTurns--;
            EffectSystem.TickEffects(creature);
            return true;
        }
        return false;
    }

    /// <summary>Вернуть true если creature бездействует.</summary>
    private static bool CheckDirectionNone(Creature creature, Direction direction)
    {
        if (direction == Direction.None)
        {
            EffectSystem.TickEffects(creature);
            return true;
        }
        return false;
    }

    /// <summary>Переместить существо на указанную клетку. Обновляет списки существ на тайлах.</summary>
    private static void MoveTo(Creature creature, Tile targetTile)
    {
        creature.CurrentTile?.CreaturesOnTile.Remove(creature);
        creature.CurrentTile = targetTile;
        targetTile.CreaturesOnTile.Add(creature);
    }
}