namespace Rogue.Domain.World;

/// <summary>
/// Карта одного уровня подземелья.
/// </summary>
internal class Map
{
    /// <summary>Высота карты в клетках.</summary>
    internal const int Height = 30;

    /// <summary>Ширина карты в клетках.</summary>
    internal const int Width = 90;

    /// <summary>Двумерный массив клеток [y, x].</summary>
    internal Tile[,] Tiles { get; set; }

    /// <summary>Уровень, которому принадлежит карта.</summary>
    internal Level? Level { get; set; }

    internal Map()
    {
        Tiles = new Tile[Height, Width];
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                Tiles[y, x] = new Tile { X = x, Y = y };
    }

    /// <summary>Получить тайл по координатам. Возвращает null при выходе за границы.</summary>
    internal Tile? GetTile(int x, int y) => IsInBounds(x, y) ? Tiles[y, x] : null;

    /// <summary>Проверить, находятся ли координаты в границах карты.</summary>
    private static bool IsInBounds(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
}