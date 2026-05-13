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
            // TODO: заменить на FogOfWarSystem.IsVisible
            if (IsVisible(monster, enemy)) return GetDirectionToEnemy(monster, enemy, failedDirections);
        }
        return GetIdleAction(monster, failedDirections);
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

    /// <summary>Заглушка проверки видимости. TODO: заменить на FogOfWarSystem.</summary>
    private static bool IsVisible(Creature viewer, Creature target)
    {
        return true;
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

    /// <summary>Получить idle-действие согласно интерфейсам движения.</summary>
    private static GameAction GetIdleAction(Monster monster, List<GameAction> failedDirections)
    {
        if (monster is ITeleport)
        {
            // TODO: телепортация в случайную точку комнаты
            return GameAction.None;
        }
        if (monster is IDiagonalWalk)
        {
            var diagonalActions = new[] { GameAction.MoveUpLeft, GameAction.MoveUpRight, GameAction.MoveDownLeft, GameAction.MoveDownRight };
            var available = diagonalActions.Where(a => !failedDirections.Contains(a)).ToList();
            if (available.Count > 0) return available[Random.Shared.Next(available.Count)];
            return GameAction.None;
        }
        var orthogonalActions = new[] { GameAction.MoveUp, GameAction.MoveDown, GameAction.MoveLeft, GameAction.MoveRight };
        var availableOrth = orthogonalActions.Where(a => !failedDirections.Contains(a)).ToList();
        if (availableOrth.Count > 0) return availableOrth[Random.Shared.Next(availableOrth.Count)];
        return GameAction.None;
    }
}