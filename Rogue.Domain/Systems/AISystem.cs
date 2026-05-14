using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Creatures.Interfaces;

namespace Rogue.Domain.Systems;

/// <summary>
/// Система искусственного интеллекта: определяет действия существ.
/// </summary>
internal static class AISystem
{
    /// <summary>Определить действие существа с учётом неудачных направлений.</summary>
    internal static GameAction GetCreatureAction(Creature creature, List<GameAction> failedDirections)
    {
        if (creature is Monster monster) return GetMonsterAction(monster, failedDirections);
        return GameAction.None;
    }

    /// <summary>Определить действие монстра.</summary>
    private static GameAction GetMonsterAction(Monster monster, List<GameAction> failedDirections)
    {
        var enemy = MonsterSeeksEnemy(monster);
        if (enemy != null)
        {
            if (FogOfWarSystem.IsCreatureVisible(monster, enemy)) return GetDirectionToEnemy(monster, enemy, failedDirections);
        }
        return GetPatrolAction(monster, failedDirections);
    }

    /// <summary>Монстр ищет ближайшего врага в радиусе враждебности.</summary>
    private static Creature? MonsterSeeksEnemy(Monster monster)
    {
        var level = monster.CurrentTile?.Level;
        if (level == null) return null;
        Creature? closestEnemy = null;
        int closestDistance = int.MaxValue;
        foreach (var creature in level.HeroParty)
        {
            if (!creature.IsAlive) continue;
            if (RelationSystem.GetRelation(monster.Faction, creature.Faction) != Relation.Hostile) continue;
            int distance = Math.Abs(monster.CurrentTile!.X - creature.CurrentTile!.X) + Math.Abs(monster.CurrentTile!.Y - creature.CurrentTile!.Y);
            if (distance <= monster.Hostility && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = creature;
            }
        }
        return closestEnemy;
    }

    /// <summary>Получить направление к врагу, исключая неудачные.</summary>
    private static GameAction GetDirectionToEnemy(Creature chaser, Creature target, List<GameAction> failedDirections)
    {
        int dx = target.CurrentTile!.X - chaser.CurrentTile!.X;
        int dy = target.CurrentTile!.Y - chaser.CurrentTile!.Y;
        bool canDiagonal = chaser is IDiagonalWalk;
        // Вплотную
        if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
        {
            if (canDiagonal && dx < 0 && dy < 0 && !failedDirections.Contains(GameAction.MoveUpLeft)) return GameAction.MoveUpLeft;
            if (canDiagonal && dx < 0 && dy > 0 && !failedDirections.Contains(GameAction.MoveDownLeft)) return GameAction.MoveDownLeft;
            if (canDiagonal && dx > 0 && dy < 0 && !failedDirections.Contains(GameAction.MoveUpRight)) return GameAction.MoveUpRight;
            if (canDiagonal && dx > 0 && dy > 0 && !failedDirections.Contains(GameAction.MoveDownRight)) return GameAction.MoveDownRight;
            if (dx < 0 && !failedDirections.Contains(GameAction.MoveLeft)) return GameAction.MoveLeft;
            if (dx > 0 && !failedDirections.Contains(GameAction.MoveRight)) return GameAction.MoveRight;
            if (dy < 0 && !failedDirections.Contains(GameAction.MoveUp)) return GameAction.MoveUp;
            if (dy > 0 && !failedDirections.Contains(GameAction.MoveDown)) return GameAction.MoveDown;
            return GameAction.None;
        }
        // На расстоянии
        if (canDiagonal && Math.Abs(dx) >= 1 && Math.Abs(dy) >= 1)
        {
            if (dx < 0 && dy < 0 && !failedDirections.Contains(GameAction.MoveUpLeft)) return GameAction.MoveUpLeft;
            if (dx < 0 && dy > 0 && !failedDirections.Contains(GameAction.MoveDownLeft)) return GameAction.MoveDownLeft;
            if (dx > 0 && dy < 0 && !failedDirections.Contains(GameAction.MoveUpRight)) return GameAction.MoveUpRight;
            if (dx > 0 && dy > 0 && !failedDirections.Contains(GameAction.MoveDownRight)) return GameAction.MoveDownRight;
            return GameAction.None;
        }
        if (dx > 0 && Math.Abs(dx) >= Math.Abs(dy) && !failedDirections.Contains(GameAction.MoveRight)) return GameAction.MoveRight;
        if (dx < 0 && Math.Abs(dx) >= Math.Abs(dy) && !failedDirections.Contains(GameAction.MoveLeft)) return GameAction.MoveLeft;
        if (dy > 0 && Math.Abs(dy) > Math.Abs(dx) && !failedDirections.Contains(GameAction.MoveDown)) return GameAction.MoveDown;
        if (dy < 0 && Math.Abs(dy) > Math.Abs(dx) && !failedDirections.Contains(GameAction.MoveUp)) return GameAction.MoveUp;
        return GameAction.None;
    }

    /// <summary>Получить действие патрулирования согласно интерфейсам движения.</summary>
    private static GameAction GetPatrolAction(Monster monster, List<GameAction> failedDirections)
    {
        if (monster is ITeleport) return TryTeleport(monster);
        if (monster is IDiagonalWalk) return GetRandomDiagonalAction(failedDirections);
        return GetRandomOrthogonalAction(failedDirections);
    }

    /// <summary>Попытаться телепортироваться на случайный видимый тайл.</summary>
    private static GameAction TryTeleport(Monster monster)
    {
        var level = monster.CurrentTile?.Level;
        if (level == null) return GameAction.None;
        for (int attempt = 0; attempt < 10; attempt++)
        {
            int dx = Random.Shared.Next(-monster.Hostility, monster.Hostility + 1);
            int dy = Random.Shared.Next(-monster.Hostility, monster.Hostility + 1);
            int targetX = monster.CurrentTile!.X + dx;
            int targetY = monster.CurrentTile!.Y + dy;
            var targetTile = level.Map.GetTile(targetX, targetY);
            if (targetTile != null && targetTile.IsWalkable && targetTile.CreaturesOnTile.All(c => !c.IsAlive) && FogOfWarSystem.IsTileVisible(monster, targetX, targetY))
            {
                monster.CurrentTile.CreaturesOnTile.Remove(monster);
                monster.CurrentTile = targetTile;
                targetTile.CreaturesOnTile.Add(monster);
                return GameAction.None;
            }
        }
        return GameAction.None;
    }

    /// <summary>Случайное диагональное направление.</summary>
    private static GameAction GetRandomDiagonalAction(List<GameAction> failedDirections)
    {
        var diagonalActions = new[] { GameAction.MoveUpLeft, GameAction.MoveUpRight, GameAction.MoveDownLeft, GameAction.MoveDownRight };
        var available = diagonalActions.Where(a => !failedDirections.Contains(a)).ToList();
        if (available.Count > 0) return available[Random.Shared.Next(available.Count)];
        return GameAction.None;
    }

    /// <summary>Случайное ортогональное направление.</summary>
    private static GameAction GetRandomOrthogonalAction(List<GameAction> failedDirections)
    {
        var orthogonalActions = new[] { GameAction.MoveUp, GameAction.MoveDown, GameAction.MoveLeft, GameAction.MoveRight };
        var available = orthogonalActions.Where(a => !failedDirections.Contains(a)).ToList();
        if (available.Count > 0) return available[Random.Shared.Next(available.Count)];
        return GameAction.None;
    }
}