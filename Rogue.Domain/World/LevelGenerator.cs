namespace Rogue.Domain.World;

/// <summary>
/// Генератор уровня. Собирает воедино структуру подземелья,
/// проверку связности (при случайном количестве комнат) и расстановку сущностей.
/// </summary>
public static class LevelGenerator
{
    /// <summary>Создать полностью готовый уровень с указанным номером.</summary>
    public static Level Generate(int levelNumber)
    {
        var rooms = new List<Room>();
        var corridors = new List<Corridor>();
        // Генерация структуры. При случайном количестве комнат — повторяем до связности
        do
        {
            DungeonGenerator.Generate(rooms, corridors);
        } while (DungeonGenerator.RoomChancePercent < 100 && !ConnectivityChecker.Check(rooms));
        // Создание уровня и заполнение сущностями
        var level = new Level
        {
            LevelNumber = levelNumber,
            Rooms = rooms,
            Corridors = corridors
        };
        // Привязываем Level ко всем тайлам и карте
        level.Map.Level = level;
        for (int y = 0; y < Map.Height; y++)
            for (int x = 0; x < Map.Width; x++)
                level.Map.Tiles[y, x].Level = level;
        EntityGenerator.Generate(level);
        return level;
    }
}