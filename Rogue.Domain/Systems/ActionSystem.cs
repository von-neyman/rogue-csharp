using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Food;
using Rogue.Domain.Entities.Items.Potions;
using Rogue.Domain.Entities.Items.Scrolls;
using Rogue.Domain.Entities.Items.Treasures;
using Rogue.Domain.Entities.Items.Weapons;
using Rogue.Domain.World;

namespace Rogue.Domain.Systems;

/// <summary>
/// Система действий: обработка команд существ, отложенные действия, ход монстров.
/// </summary>
public static class ActionSystem
{
    /// <summary>Событие: игрок съел еду.</summary>
    public static event Action? OnFoodEaten;

    /// <summary>Событие: игрок выпил зелье.</summary>
    public static event Action? OnPotionUsed;

    /// <summary>Событие: игрок прочитал свиток.</summary>
    public static event Action? OnScrollRead;

    /// <summary>Событие: игрок сделал шаг.</summary>
    public static event Action? OnStepTaken;

    /// <summary>Выполнить действие существа. Возвращает true, если действие совершено (ход завершён).</summary>
    public static bool CreatureAction(Creature creature, ref GameAction gameAction)
    {
        EffectSystem.TickEffects(creature);
        if (CheckSkipTurns(creature)) gameAction = GameAction.None;
        if (creature.FirstPendingAction != GameAction.None)
        {
            if (!IsValidPendingChain(creature, gameAction))
            {
                creature.FirstPendingAction = GameAction.None;
                creature.SecondPendingAction = GameAction.None;
            }
        }
        if (gameAction == GameAction.None) return true;
        if (IsMoveAction(gameAction))
        {
            bool actionSucceeded = MovementSystem.PerformAction(creature, gameAction);
            if (actionSucceeded)
            {
                UpdateStepStatistics(creature);
                gameAction = GameAction.None;
            }
            return actionSucceeded;
        }
        return ExecutePendingAction(creature, ref gameAction);
    }

    /// <summary>Проверить, соответствует ли новое действие цепочке отложенных.</summary>
    private static bool IsValidPendingChain(Creature creature, GameAction gameAction)
    {
        if (creature.FirstPendingAction == GameAction.DropItem)
        {
            if (creature.SecondPendingAction == GameAction.None) return IsItemTypeAction(gameAction);
            if (creature.SecondPendingAction != GameAction.None) return IsSlotAction(gameAction);
        }
        if (IsItemTypeAction(creature.FirstPendingAction))
        {
            if (creature.SecondPendingAction == GameAction.None) return IsSlotAction(gameAction);
        }
        return false;
    }

    /// <summary>Выполнить отложенное действие.</summary>
    private static bool ExecutePendingAction(Creature creature, ref GameAction gameAction)
    {
        if (gameAction == GameAction.DropItem && creature is IInventory)
        {
            creature.FirstPendingAction = GameAction.DropItem;
            gameAction = GameAction.None;
            return false;
        }
        if (IsItemTypeAction(gameAction) && creature is IInventory)
        {
            if (creature.FirstPendingAction == GameAction.DropItem) creature.SecondPendingAction = gameAction;
            else creature.FirstPendingAction = gameAction;
            gameAction = GameAction.None;
            return false;
        }
        if (IsSlotAction(gameAction))
        {
            int slotIndex = GetSlotIndex(gameAction);
            bool pendingActionSucceeded = false;
            if (creature.FirstPendingAction == GameAction.DropItem && creature.SecondPendingAction != GameAction.None)
                pendingActionSucceeded = DropByType(creature, creature.SecondPendingAction, slotIndex);
            else if (IsItemTypeAction(creature.FirstPendingAction))
            {
                pendingActionSucceeded = UseByType(creature, creature.FirstPendingAction, slotIndex);
                if (pendingActionSucceeded) UpdateUsageStatistics(creature, creature.FirstPendingAction);
            }
            creature.FirstPendingAction = GameAction.None;
            creature.SecondPendingAction = GameAction.None;
            gameAction = GameAction.None;
            return pendingActionSucceeded;
        }
        creature.FirstPendingAction = GameAction.None;
        creature.SecondPendingAction = GameAction.None;
        gameAction = GameAction.None;
        return false;
    }

    /// <summary>Обновить статистику шагов.</summary>
    private static void UpdateStepStatistics(Creature creature)
    {
        if (creature is Hero) OnStepTaken?.Invoke();
    }

    /// <summary>Обновить статистику использования предметов.</summary>
    private static void UpdateUsageStatistics(Creature creature, GameAction typeAction)
    {
        if (creature is not Hero) return;
        switch (typeAction)
        {
            case GameAction.UseFood: OnFoodEaten?.Invoke(); break;
            case GameAction.UsePotion: OnPotionUsed?.Invoke(); break;
            case GameAction.UseScroll: OnScrollRead?.Invoke(); break;
        }
    }

    /// <summary>Использовать предмет по типу отложенного действия.</summary>
    private static bool UseByType(Creature creature, GameAction typeAction, int slotIndex)
    {
        return typeAction switch
        {
            GameAction.UseFood => ((IInventory)creature).UseItem<Food>(slotIndex),
            GameAction.UsePotion => ((IInventory)creature).UseItem<Potion>(slotIndex),
            GameAction.UseScroll => ((IInventory)creature).UseItem<Scroll>(slotIndex),
            GameAction.UseWeapon => ((IInventory)creature).UseItem<Weapon>(slotIndex),
            _ => false
        };
    }

    /// <summary>Выбросить предмет по типу отложенного действия.</summary>
    private static bool DropByType(Creature creature, GameAction typeAction, int slotIndex)
    {
        return typeAction switch
        {
            GameAction.UseFood => ((IInventory)creature).DropItem<Food>(slotIndex),
            GameAction.UsePotion => ((IInventory)creature).DropItem<Potion>(slotIndex),
            GameAction.UseScroll => ((IInventory)creature).DropItem<Scroll>(slotIndex),
            GameAction.UseWeapon => ((IInventory)creature).DropItem<Weapon>(slotIndex),
            _ => ((IInventory)creature).DropItem<Treasure>(slotIndex)
        };
    }

    /// <summary>Ход всех монстров на уровне.</summary>
    public static void MonstersAction(Level level)
    {
        if (level.Hero == null) return;
        var orderedMonsters = level.Monsters.Where(m => m.IsAlive).OrderByDescending(m => m.Agility).ToList();
        foreach (var monster in orderedMonsters)
        {
            if (!monster.IsAlive) continue;
            GameAction monsterAction = GetMonsterAction(monster, level.Hero);
            while (!CreatureAction(monster, ref monsterAction))
            {
                monsterAction = GetMonsterAction(monster, level.Hero);
            }
        }
    }

    /// <summary>Определить действие монстра.</summary>
    private static GameAction GetMonsterAction(Monster monster, Hero hero)
    {
        // TODO: chase + idle-паттерны
        return Random.Shared.Next(5) switch
        {
            0 => GameAction.None,
            1 => GameAction.MoveUp,
            2 => GameAction.MoveDown,
            3 => GameAction.MoveLeft,
            _ => GameAction.MoveRight
        };
    }

    private static bool CheckSkipTurns(Creature creature)
    {
        if (creature.SkipTurns > 0)
        {
            creature.SkipTurns--;
            return true;
        }
        return false;
    }

    private static bool IsMoveAction(GameAction gameAction)
    {
        return gameAction == GameAction.MoveUp || gameAction == GameAction.MoveDown
            || gameAction == GameAction.MoveLeft || gameAction == GameAction.MoveRight
            || gameAction == GameAction.MoveUpLeft || gameAction == GameAction.MoveUpRight
            || gameAction == GameAction.MoveDownLeft || gameAction == GameAction.MoveDownRight;
    }

    private static bool IsItemTypeAction(GameAction gameAction)
    {
        return gameAction == GameAction.UseFood || gameAction == GameAction.UsePotion
            || gameAction == GameAction.UseScroll || gameAction == GameAction.UseWeapon;
    }

    private static bool IsSlotAction(GameAction gameAction)
    {
        return gameAction >= GameAction.SelectSlot0 && gameAction <= GameAction.SelectSlot9;
    }

    private static int GetSlotIndex(GameAction gameAction)
    {
        return gameAction switch
        {
            GameAction.SelectSlot0 => 0,
            GameAction.SelectSlot1 => 1,
            GameAction.SelectSlot2 => 2,
            GameAction.SelectSlot3 => 3,
            GameAction.SelectSlot4 => 4,
            GameAction.SelectSlot5 => 5,
            GameAction.SelectSlot6 => 6,
            GameAction.SelectSlot7 => 7,
            GameAction.SelectSlot8 => 8,
            GameAction.SelectSlot9 => 9,
            _ => 0
        };
    }
}