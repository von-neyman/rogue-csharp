using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.World;

namespace Rogue.Domain.Systems;

/// <summary>
/// Система передвижения. Обрабатывает движение и атаку существ.
/// </summary>
internal static class MovementSystem
{
    /// <summary>
    /// Выполнить движение или атаку существа. Возвращает true, если действие состоялось.
    /// </summary>
    internal static bool PerformAction(Creature creature, GameAction gameAction)
    {
        int stepsAvailable = GetMaxSteps(creature);
        GameAction initialAction = gameAction;
        while (stepsAvailable > 0)
        {
            (int targetX, int targetY) = GetTargetPosition(creature, gameAction);
            if (targetX == -1) return false;
            var targetTile = creature.CurrentTile?.Level?.Map.GetTile(targetX, targetY);
            if (targetTile == null || !targetTile.IsWalkable) return DidAnyStep(stepsAvailable, creature);
            var targetCreature = GetCreatureAt(targetTile);
            if (targetCreature != null && creature is ICanAttack attacker)
            {
                if (AreNotEnemies(creature, targetCreature)) return DidAnyStep(stepsAvailable, creature);
                attacker.Attack(targetCreature);
                stepsAvailable = 0;
            }
            else
            {
                MoveTo(creature, targetTile);
                if (creature is IInventory collector) collector.CollectItems();
                stepsAvailable--;
                gameAction = initialAction;
            }
        }
        return true;
    }

    /// <summary>Вычислить координаты клетки в указанном направлении от существа.</summary>
    private static (int X, int Y) GetTargetPosition(Creature creature, GameAction gameAction)
    {
        if (creature.CurrentTile == null) return (-1, -1);
        int x = creature.CurrentTile.X;
        int y = creature.CurrentTile.Y;
        return gameAction switch
        {
            GameAction.MoveUp => (x, y - 1),
            GameAction.MoveDown => (x, y + 1),
            GameAction.MoveLeft => (x - 1, y),
            GameAction.MoveRight => (x + 1, y),
            GameAction.MoveUpLeft => (x - 1, y - 1),
            GameAction.MoveUpRight => (x + 1, y - 1),
            GameAction.MoveDownLeft => (x - 1, y + 1),
            GameAction.MoveDownRight => (x + 1, y + 1),
            _ => (x, y)
        };
    }

    /// <summary>Найти первое живое существо на клетке.</summary>
    private static Creature? GetCreatureAt(Tile tile)
    {
        return tile.CreaturesOnTile.FirstOrDefault(c => c.IsAlive);
    }

    /// <summary>Получить максимальное количество шагов для существа.</summary>
    private static int GetMaxSteps(Creature creature)
    {
        return creature is IDoubleStepWalk ? 2 : 1;
    }

    /// <summary>Проверить, был ли сделан хотя бы один шаг.</summary>
    private static bool DidAnyStep(int stepsAvailable, Creature creature)
    {
        return stepsAvailable < GetMaxSteps(creature);
    }

    /// <summary>Проверить, что два существа не являются врагами.</summary>
    private static bool AreNotEnemies(Creature first, Creature second)
    {
        return RelationSystem.GetRelation(first.Faction, second.Faction) != Relation.Hostile;
    }

    /// <summary>Переместить существо на указанную клетку. Обновляет списки существ на тайлах.</summary>
    private static void MoveTo(Creature creature, Tile targetTile)
    {
        creature.CurrentTile?.CreaturesOnTile.Remove(creature);
        creature.CurrentTile = targetTile;
        targetTile.CreaturesOnTile.Add(creature);
    }
}