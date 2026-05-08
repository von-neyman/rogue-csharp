namespace Rogue.Domain.World;

/// <summary>
/// Карта одного уровня подземелья.
/// Содержит двумерный массив клеток и методы доступа с проверкой границ.
/// </summary>
public class Map
{
    /// <summary>Высота карты в клетках.</summary>
    public const int Height = 30;

    /// <summary>Ширина карты в клетках.</summary>
    public const int Width = 90;

    /// <summary>Двумерный массив клеток [y, x].</summary>
    public Tile[,] Tiles { get; set; }

    public Map()
    {
        Tiles = new Tile[Height, Width];
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                Tiles[y, x] = new Tile();
    }

    /// <summary>Получить клетку по координатам.</summary>
    public Tile GetTile(int x, int y) => Tiles[y, x];

    /// <summary>Можно ли пройти по клетке (координаты в границах карты, пол или коридор, не стена).</summary>
    public bool IsWalkable(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return false;
        return Tiles[y, x].IsWalkable;
    }

    /// <summary>Проходит ли свет через клетку (для Ray Casting).</summary>
    public bool IsTransparent(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height) return false;
        return Tiles[y, x].IsTransparent;
    }
}