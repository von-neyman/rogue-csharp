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
    public static bool PerformAction(Creature creature, Direction direction, Level level)
    {
        if (CheckSkipTurns(creature)) return true;
        if (CheckDirectionNone(creature, direction)) return true;
        (int targetX, int targetY) = GetTargetPosition(creature, direction);
        if (targetX == -1 || !level.Map.IsWalkable(targetX, targetY)) return false;
        var targetTile = level.Map.GetTile(targetX, targetY);
        var targetCreature = GetCreatureAt(targetTile);
        if (targetCreature != null && creature is ICanAttack attacker) attacker.Attack(targetCreature);
        else MoveTo(creature, targetTile);
        CheckBoost(creature);
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
            CheckBoost(creature);
            return true;
        }
        return false;
    }

    /// <summary>Вернуть true если creature бездействует.</summary>
    private static bool CheckDirectionNone(Creature creature, Direction direction)
    {
        if (direction == Direction.None)
        {
            CheckBoost(creature);
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

    /// <summary>Обработка счётчиков эффектов существа.</summary>
    private static void CheckBoost(Creature creature)
    {
        if (creature.StrengthBoostTurns > 0)
        {
            creature.StrengthBoostTurns--;
            if (creature.StrengthBoostTurns == 0) creature.Strength = creature.BaseStrength;
        }
        if (creature.AgilityBoostTurns > 0)
        {
            creature.AgilityBoostTurns--;
            if (creature.AgilityBoostTurns == 0) creature.Agility = creature.BaseAgility;
        }
        if (creature.HealthBoostTurns > 0)
        {
            creature.HealthBoostTurns--;
            if (creature.HealthBoostTurns == 0)
            {
                creature.MaxHealth = creature.BaseMaxHealth;
                creature.Health -= creature.BaseMaxHealth;
                if (creature.Health <= 0) creature.Health = 1;
            }
        }
    }
}