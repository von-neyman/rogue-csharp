using Rogue.Domain.World;

namespace Rogue.Domain.Systems;

/// <summary>
/// Построение карты: переносит геометрию комнат и коридоров в матрицу тайлов.
/// </summary>
internal static class BuildMapSystem
{
    private const char WallChar = '#';
    private const char CorridorChar = '+';
    private const char OuterAreaChar = '.';
    private const char InnerAreaChar = ' ';
    private const char ExitChar = '|';

    /// <summary>Заполнить карту уровня по данным комнат и коридоров.</summary>
    internal static void Build(Level level)
    {
        BackgroundToMap(level.Map);
        RoomsToMap(level);
        CorridorsToMap(level);
    }

    /// <summary>Заполнить всю карту символом внешней области.</summary>
    private static void BackgroundToMap(Map map)
    {
        for (int y = 0; y < Map.Height; y++)
            for (int x = 0; x < Map.Width; x++)
                map.Tiles[y, x].Symbol = OuterAreaChar;
    }

    /// <summary>Нарисовать все комнаты на карте.</summary>
    private static void RoomsToMap(Level level)
    {
        foreach (var room in level.Rooms)
        {
            DrawRectangle(level.Map, room.TopLeft, room.BottomRight);
            InsertDoors(level.Map, room);
        }
        level.Map.GetTile(level.ExitPoint.X, level.ExitPoint.Y).Symbol = ExitChar;
    }

    /// <summary>Нарисовать прямоугольник комнаты: стены, пол.</summary>
    private static void DrawRectangle(Map map, (int X, int Y) topLeft, (int X, int Y) bottomRight)
    {
        int topY = topLeft.Y, leftX = topLeft.X, bottomY = bottomRight.Y, rightX = bottomRight.X;
        for (int x = leftX; x <= rightX; x++)
        {
            map.GetTile(x, topY).Symbol = WallChar;
            map.GetTile(x, bottomY).Symbol = WallChar;
        }
        for (int y = topY + 1; y < bottomY; y++)
        {
            map.GetTile(leftX, y).Symbol = WallChar;
            map.GetTile(rightX, y).Symbol = WallChar;
        }
        for (int y = topY + 1; y < bottomY; y++)
            for (int x = leftX + 1; x < rightX; x++)
            {
                var tile = map.GetTile(x, y);
                tile.Symbol = InnerAreaChar;
                tile.IsWalkable = true;
                tile.IsTransparent = true;
            }
    }

    /// <summary>Вставить дверные проёмы на месте стен.</summary>
    private static void InsertDoors(Map map, Room room)
    {
        foreach (var door in room.Doors)
        {
            if (door.X == -1) continue;
            var tile = map.GetTile(door.X, door.Y);
            tile.Symbol = CorridorChar;
            tile.IsWalkable = true;
            tile.IsTransparent = true;
        }
    }

    /// <summary>Нарисовать все коридоры на карте.</summary>
    private static void CorridorsToMap(Level level)
    {
        foreach (var corridor in level.Corridors)
        {
            for (int i = 0; i < corridor.Points.Count - 1; i++)
            {
                var firstPoint = corridor.Points[i];
                var secondPoint = corridor.Points[i + 1];
                if (firstPoint.X == secondPoint.X) DrawVerticalLine(level.Map, firstPoint, secondPoint);
                else DrawHorizontalLine(level.Map, firstPoint, secondPoint);
            }
        }
    }

    /// <summary>Нарисовать вертикальную линию коридора.</summary>
    private static void DrawVerticalLine(Map map, (int X, int Y) first, (int X, int Y) second)
    {
        int x = first.X;
        int minY = Math.Min(first.Y, second.Y);
        int maxY = Math.Max(first.Y, second.Y);
        for (int y = minY; y <= maxY; y++) SetCorridor(map, x, y);
    }

    /// <summary>Нарисовать горизонтальную линию коридора.</summary>
    private static void DrawHorizontalLine(Map map, (int X, int Y) first, (int X, int Y) second)
    {
        int y = first.Y;
        int minX = Math.Min(first.X, second.X);
        int maxX = Math.Max(first.X, second.X);
        for (int x = minX; x <= maxX; x++) SetCorridor(map, x, y);
    }

    /// <summary>Установить клетку как коридор.</summary>
    private static void SetCorridor(Map map, int x, int y)
    {
        var tile = map.GetTile(x, y);
        tile.Symbol = CorridorChar;
        tile.IsWalkable = true;
        tile.IsTransparent = true;
    }
}