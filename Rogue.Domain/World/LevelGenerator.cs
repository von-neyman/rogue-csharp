using Rogue.Domain.Systems;

namespace Rogue.Domain.World;

/// <summary>
/// Генератор уровня. Собирает воедино структуру подземелья,
/// проверку связности (при случайном количестве комнат) и расстановку сущностей.
/// </summary>
internal static class LevelGenerator
{
    /// <summary>Создать полностью готовый уровень с указанным номером.</summary>
    internal static Level Generate(int levelNumber)
    {
        var rooms = new List<Room>();
        var corridors = new List<Corridor>();
        do
        {
            DungeonGenerator.Generate(rooms, corridors);
        } while (DungeonGenerator.RoomChancePercent < 100 && !ConnectivityChecker.Check(rooms));
        var level = new Level
        {
            LevelNumber = levelNumber,
            Rooms = rooms,
            Corridors = corridors
        };
        level.Map.Level = level;
        for (int y = 0; y < Map.Height; y++)
            for (int x = 0; x < Map.Width; x++)
                level.Map.Tiles[y, x].Level = level;
        EntityGenerator.Generate(level);
        BuildMapSystem.Build(level);
        return level;
    }
}