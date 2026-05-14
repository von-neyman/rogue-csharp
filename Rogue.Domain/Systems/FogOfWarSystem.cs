using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.World;

namespace Rogue.Domain.Systems;

/// <summary>
/// Система тумана войны: видимость тайлов и существ.
/// Использует алгоритм Брезенхэма для проверки прямой видимости.
/// </summary>
internal static class FogOfWarSystem
{
    private const int ViewDistance = 20;

    /// <summary>Обновить видимость тайлов вокруг игрока.</summary>
    internal static void UpdateVisibility(Level level)
    {
        if (level.Hero?.CurrentTile == null) return;
        for (int y = 0; y < Map.Height; y++)
            for (int x = 0; x < Map.Width; x++)
                level.Map.Tiles[y, x].IsVisible = false;
        int centerX = level.Hero.CurrentTile.X;
        int centerY = level.Hero.CurrentTile.Y;
        for (int angleDegrees = 0; angleDegrees < 360; angleDegrees += 3)
        {
            double radians = angleDegrees * Math.PI / 180.0;
            int targetX = centerX + (int)(Math.Cos(radians) * ViewDistance);
            int targetY = centerY + (int)(Math.Sin(radians) * ViewDistance);
            MarkVisibleLine(level.Map, centerX, centerY, targetX, targetY);
        }
    }

    /// <summary>Видит ли viewer тайл (targetX, targetY).</summary>
    internal static bool IsTileVisible(Creature viewer, int targetX, int targetY)
    {
        if (viewer.CurrentTile == null) return false;
        return HasLineOfSight(viewer.CurrentTile.Level?.Map, viewer.CurrentTile.X, viewer.CurrentTile.Y, targetX, targetY);
    }

    /// <summary>Видит ли viewer существо target.</summary>
    internal static bool IsCreatureVisible(Creature viewer, Creature target)
    {
        if (viewer.CurrentTile == null || target.CurrentTile == null) return false;
        return HasLineOfSight(viewer.CurrentTile.Level?.Map, viewer.CurrentTile.X, viewer.CurrentTile.Y, target.CurrentTile.X, target.CurrentTile.Y);
    }

    /// <summary>Отметить все тайлы на линии от (startX,startY) до (endX,endY) как видимые.</summary>
    private static void MarkVisibleLine(Map map, int startX, int startY, int endX, int endY)
    {
        foreach (var (x, y) in BresenhamLine(startX, startY, endX, endY))
        {
            var tile = map.GetTile(x, y);
            if (tile == null) return;
            tile.IsVisible = true;
            tile.IsExplored = true;
            if (!tile.IsTransparent) return;
        }
    }

    /// <summary>Проверить прямую видимость между двумя точками.</summary>
    private static bool HasLineOfSight(Map? map, int startX, int startY, int endX, int endY)
    {
        if (map == null) return false;
        foreach (var (x, y) in BresenhamLine(startX, startY, endX, endY))
        {
            var tile = map.GetTile(x, y);
            if (tile == null) return false;
            if (!tile.IsTransparent && (x != endX || y != endY)) return false;
        }
        return true;
    }

    /// <summary>
    /// Алгоритм Брезенхэма: перечисление тайлов на линии от (startX,startY) до (endX,endY).
    /// </summary>
    private static IEnumerable<(int X, int Y)> BresenhamLine(int startX, int startY, int endX, int endY)
    {
        int absoluteDeltaX = Math.Abs(endX - startX);
        int negativeAbsoluteDeltaY = -Math.Abs(endY - startY);
        int stepX = startX < endX ? 1 : -1;
        int stepY = startY < endY ? 1 : -1;
        int accumulatedError = absoluteDeltaX + negativeAbsoluteDeltaY;
        int currentX = startX, currentY = startY;
        while (true)
        {
            yield return (currentX, currentY);
            if (currentX == endX && currentY == endY) break;
            int doubledError = 2 * accumulatedError;
            if (doubledError >= negativeAbsoluteDeltaY)
            {
                if (currentX == endX) break;
                accumulatedError += negativeAbsoluteDeltaY;
                currentX += stepX;
            }
            if (doubledError <= absoluteDeltaX)
            {
                if (currentY == endY) break;
                accumulatedError += absoluteDeltaX;
                currentY += stepY;
            }
        }
    }
}