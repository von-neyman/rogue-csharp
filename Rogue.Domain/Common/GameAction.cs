namespace Rogue.Domain.Common;

/// <summary>
/// Действия, которые может совершить существо.
/// </summary>
public enum GameAction
{
    /// <summary>Выбрать предмет из списка инвентаря (слот 0) - 0.</summary>
    SelectSlot0,
    /// <summary>Выбрать предмет из списка инвентаря (слот 1) - 1.</summary>
    SelectSlot1,
    /// <summary>Выбрать предмет из списка инвентаря (слот 2) - 2.</summary>
    SelectSlot2,
    /// <summary>Выбрать предмет из списка инвентаря (слот 3) - 3.</summary>
    SelectSlot3,
    /// <summary>Выбрать предмет из списка инвентаря (слот 4) - 4.</summary>
    SelectSlot4,
    /// <summary>Выбрать предмет из списка инвентаря (слот 5) - 5.</summary>
    SelectSlot5,
    /// <summary>Выбрать предмет из списка инвентаря (слот 6) - 6.</summary>
    SelectSlot6,
    /// <summary>Выбрать предмет из списка инвентаря (слот 7) - 7.</summary>
    SelectSlot7,
    /// <summary>Выбрать предмет из списка инвентаря (слот 8) - 8.</summary>
    SelectSlot8,
    /// <summary>Выбрать предмет из списка инвентаря (слот 9) - 9.</summary>
    SelectSlot9,
    /// <summary>Бездействие (пропустить ход) - N.</summary>
    None,
    /// <summary>Движение вверх - W.</summary>
    MoveUp,
    /// <summary>Движение вниз - S.</summary>
    MoveDown,
    /// <summary>Движение влево - A.</summary>
    MoveLeft,
    /// <summary>Движение вправо - D.</summary>
    MoveRight,
    /// <summary>Движение по диагонали вверх-влево.</summary>
    MoveUpLeft,
    /// <summary>Движение по диагонали вверх-вправо.</summary>
    MoveUpRight,
    /// <summary>Движение по диагонали вниз-влево.</summary>
    MoveDownLeft,
    /// <summary>Движение по диагонали вниз-вправо.</summary>
    MoveDownRight,
    /// <summary>Использовать еду из инвентаря - J.</summary>
    UseFood,
    /// <summary>Использовать зелье из инвентаря - K.</summary>
    UsePotion,
    /// <summary>Использовать свиток из инвентаря - E.</summary>
    UseScroll,
    /// <summary>Экипировать оружие из инвентаря - H.</summary>
    UseWeapon,
    /// <summary>Выбросить предмет из инвентаря - D.</summary>
    DropItem,
    /// <summary>Выйти из игры с автосохранением - Q.</summary>
    Quit
}