namespace Rogue.Domain.World;

/// <summary>
/// Генератор структуры подземелья: создаёт комнаты, связи между ними и коридоры.
/// </summary>
public static class DungeonGenerator
{
    // Количество секторов по каждой стороне сетки (3×3)
    private const int RoomsPerSide = 3;
    // Высота одного сектора в клетках (Map.Height / RoomsPerSide = 30 / 3)
    private const int SectorHeight = 10;
    // Ширина одного сектора в клетках (Map.Width / RoomsPerSide = 90 / 3)
    private const int SectorWidth = 30;
    // Диапазон случайного смещения углов комнаты по вертикали внутри сектора
    private const int CornerVerticalRange = 2;
    // Диапазон случайного смещения углов комнаты по горизонтали внутри сектора
    private const int CornerHorizontalRange = 12;

    /// <summary>Генератор случайных чисел для всего класса.</summary>
    private static readonly Random RandomGenerator = new();

    /// <summary>Вероятность создания комнаты в каждом секторе в процентах (100 — всегда 9 комнат, 50 — случайно от 3 до 9).</summary>
    public static int RoomChancePercent { get; set; } = 100;

    // Индексы сторон в массивах Connections и Doors
    private const int Top = 0;
    private const int Right = 1;
    private const int Bottom = 2;
    private const int Left = 3;

    // Типы коридоров
    private const int CorridorLeftToRight = 0;
    private const int CorridorLeftTurn = 1;
    private const int CorridorRightTurn = 2;
    private const int CorridorTopToBottom = 3;

    /// <summary>Главный метод генерации. Заполняет переданные списки комнат и коридоров.</summary>
    public static void Generate(List<Room> rooms, List<Corridor> corridors)
    {
        rooms.Clear();
        corridors.Clear();
        // Временная сетка 3×3 для быстрого доступа к комнатам по координатам
        var grid = new Room?[RoomsPerSide, RoomsPerSide];
        CreateRooms(grid, rooms);
        CreatePrimaryConnections(grid);
        CreateSecondaryConnections(rooms);
        GenerateRoomsGeometry(rooms);
        GenerateCorridorsGeometry(corridors, grid);
    }

    /// <summary>Создаёт комнаты в сетке 3×3. При RoomChancePercent меньше 100 создаёт случайное количество,
    /// но гарантирует минимум 3 комнаты.</summary>
    private static void CreateRooms(Room?[,] grid, List<Room> rooms)
    {
        for (int row = 0; row < RoomsPerSide; row++)
            for (int col = 0; col < RoomsPerSide; col++)
                if (RandomGenerator.Next(100) < RoomChancePercent)
                {
                    var room = new Room { GridI = row, GridJ = col };
                    grid[row, col] = room;
                    rooms.Add(room);
                }
        bool needsSorting = rooms.Count < 3;
        while (rooms.Count < 3)
        {
            int row = RandomGenerator.Next(RoomsPerSide);
            int col = RandomGenerator.Next(RoomsPerSide);
            if (grid[row, col] == null)
            {
                var room = new Room { GridI = row, GridJ = col };
                grid[row, col] = room;
                rooms.Add(room);
            }
        }
        if (needsSorting) rooms.Sort((firstRoom, secondRoom) => firstRoom.Sector.CompareTo(secondRoom.Sector));
    }

    /// <summary>Соединяет соседние по сетке комнаты. Связь создаётся только если обе комнаты существуют.</summary>
    private static void CreatePrimaryConnections(Room?[,] grid)
    {
        for (int row = 0; row < RoomsPerSide; row++)
        {
            for (int col = 0; col < RoomsPerSide; col++)
            {
                var currentRoom = grid[row, col];
                if (currentRoom == null) continue;
                if (row > 0 && grid[row - 1, col] != null)
                    ConnectRooms(currentRoom, grid[row - 1, col]!, Top);
                if (row < RoomsPerSide - 1 && grid[row + 1, col] != null)
                    ConnectRooms(currentRoom, grid[row + 1, col]!, Bottom);
                if (col > 0 && grid[row, col - 1] != null)
                    ConnectRooms(currentRoom, grid[row, col - 1]!, Left);
                if (col < RoomsPerSide - 1 && grid[row, col + 1] != null)
                    ConnectRooms(currentRoom, grid[row, col + 1]!, Right);
            }
        }
    }

    /// <summary>Устанавливает двустороннюю связь между двумя комнатами.
    /// directionIndex — индекс стороны (Top=0, Right=1, Bottom=2, Left=3)
    /// для первой комнаты. Вторая получает противоположную сторону.</summary>
    private static void ConnectRooms(Room firstRoom, Room secondRoom, int directionIndex)
    {
        int oppositeIndex = GetOppositeSideIndex(directionIndex);
        firstRoom.Connections[directionIndex] = secondRoom;
        secondRoom.Connections[oppositeIndex] = firstRoom;
    }

    /// <summary>Возвращает индекс противоположной стороны.</summary>
    private static int GetOppositeSideIndex(int sideIndex)
    {
        if (sideIndex == Top) return Bottom;
        if (sideIndex == Bottom) return Top;
        if (sideIndex == Left) return Right;
        return Left;
    }

    /// <summary>Добавляет дополнительные связи, чтобы граф комнат был гарантированно связным
    /// при случайном количестве комнат.</summary>
    private static void CreateSecondaryConnections(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            var currentRoom = rooms[i];
            var nextRoom = rooms[i + 1];
            if (currentRoom.GridI == nextRoom.GridI && nextRoom.Connections[Left] == null)
            {
                ConnectRooms(currentRoom, nextRoom, Right);
            }
            else if (currentRoom.GridI - nextRoom.GridI == -1 && currentRoom.Connections[Bottom] == null)
            {
                if (currentRoom.GridJ < nextRoom.GridJ && nextRoom.Connections[Left] == null)
                    ConnectRooms(currentRoom, nextRoom, Bottom);
                else if (currentRoom.GridJ > nextRoom.GridJ && nextRoom.Connections[Right] == null)
                    ConnectRooms(currentRoom, nextRoom, Bottom);
                else if (currentRoom.GridJ > nextRoom.GridJ && currentRoom.Connections[Bottom] == null && i < rooms.Count - 2)
                    ConnectRooms(currentRoom, rooms[i + 2], Bottom);
            }
            else if (currentRoom.GridI - nextRoom.GridI == -2 && nextRoom.Connections[Top] == null)
            {
                ConnectRooms(currentRoom, nextRoom, Bottom);
            }
        }
    }

    /// <summary>Генерирует случайные координаты углов и дверей для каждой комнаты.</summary>
    private static void GenerateRoomsGeometry(List<Room> rooms)
    {
        foreach (var room in rooms)
        {
            int offsetY = room.GridI * SectorHeight;
            int offsetX = room.GridJ * SectorWidth;
            GenerateCorners(room, offsetY, offsetX);
            GenerateDoors(room);
        }
    }

    /// <summary>Случайно вычисляет левый верхний и правый нижний углы комнаты внутри её сектора.</summary>
    private static void GenerateCorners(Room room, int offsetY, int offsetX)
    {
        room.TopLeft = (
            RandomGenerator.Next(CornerHorizontalRange) + offsetX + 1,
            RandomGenerator.Next(CornerVerticalRange) + offsetY + 1
        );
        room.BottomRight = (
            SectorWidth - 1 - RandomGenerator.Next(CornerHorizontalRange) + offsetX - 1,
            SectorHeight - 1 - RandomGenerator.Next(CornerVerticalRange) + offsetY - 1
        );
    }

    /// <summary>Для каждой существующей связи создаёт дверь на соответствующей стене комнаты.</summary>
    private static void GenerateDoors(Room room)
    {
        if (room.Connections[Top] != null)
            room.Doors[Top] = (
                RandomGenerator.Next(room.BottomRight.X - room.TopLeft.X - 1) + room.TopLeft.X + 1,
                room.TopLeft.Y
            );
        if (room.Connections[Right] != null)
            room.Doors[Right] = (
                room.BottomRight.X,
                RandomGenerator.Next(room.BottomRight.Y - room.TopLeft.Y - 1) + room.TopLeft.Y + 1
            );
        if (room.Connections[Bottom] != null)
            room.Doors[Bottom] = (
                RandomGenerator.Next(room.BottomRight.X - room.TopLeft.X - 1) + room.TopLeft.X + 1,
                room.BottomRight.Y
            );
        if (room.Connections[Left] != null)
            room.Doors[Left] = (
                room.TopLeft.X,
                RandomGenerator.Next(room.BottomRight.Y - room.TopLeft.Y - 1) + room.TopLeft.Y + 1
            );
    }

    /// <summary>Генерирует геометрию коридоров для всех существующих связей между комнатами.</summary>
    private static void GenerateCorridorsGeometry(List<Corridor> corridors, Room?[,] grid)
    {
        for (int row = 0; row < RoomsPerSide; row++)
        {
            for (int col = 0; col < RoomsPerSide; col++)
            {
                var currentRoom = grid[row, col];
                if (currentRoom == null) continue;
                if (currentRoom.Connections[Right] != null && currentRoom.Connections[Right]!.Connections[Left] == currentRoom)
                    GenerateLeftToRightCorridor(currentRoom, currentRoom.Connections[Right]!, corridors, grid);
                if (currentRoom.Connections[Bottom] != null)
                {
                    var bottomRoom = currentRoom.Connections[Bottom]!;
                    int rowDifference = currentRoom.GridI - bottomRoom.GridI;
                    int colDifference = currentRoom.GridJ - bottomRoom.GridJ;
                    if (rowDifference == -1 && colDifference > 0)
                        GenerateLeftTurnCorridor(currentRoom, bottomRoom, corridors);
                    else if (rowDifference == -1 && colDifference < 0)
                        GenerateRightTurnCorridor(currentRoom, bottomRoom, corridors);
                    else if (rowDifference == -1 && colDifference == 0)
                        GenerateTopToBottomCorridor(currentRoom, bottomRoom, corridors, grid);
                }
            }
        }
    }

    /// <summary>Z-образный коридор слева направо.</summary>
    private static void GenerateLeftToRightCorridor(Room leftRoom, Room rightRoom, List<Corridor> corridors, Room?[,] grid)
    {
        var corridor = new Corridor { Type = CorridorLeftToRight };
        corridor.Points.Add(leftRoom.Doors[Right]);
        int minX = leftRoom.Doors[Right].X;
        int maxX = rightRoom.Doors[Left].X;
        for (int row = 0; row < RoomsPerSide; row++)
        {
            if (grid[row, leftRoom.GridJ] != null && row != leftRoom.GridI)
                minX = Math.Max(grid[row, leftRoom.GridJ]!.BottomRight.X, minX);
            if (grid[row, rightRoom.GridJ] != null && row != rightRoom.GridI)
                maxX = Math.Min(grid[row, rightRoom.GridJ]!.TopLeft.X, maxX);
        }
        int centerX = RandomGenerator.Next(minX + 1, maxX);
        corridor.Points.Add((centerX, leftRoom.Doors[Right].Y));
        corridor.Points.Add((centerX, rightRoom.Doors[Left].Y));
        corridor.Points.Add(rightRoom.Doors[Left]);
        corridors.Add(corridor);
    }

    /// <summary>L-образный коридор с поворотом влево: сначала вниз, потом вправо.</summary>
    private static void GenerateLeftTurnCorridor(Room topRoom, Room bottomLeftRoom, List<Corridor> corridors)
    {
        var corridor = new Corridor { Type = CorridorLeftTurn };
        corridor.Points.Add(topRoom.Doors[Bottom]);
        corridor.Points.Add((topRoom.Doors[Bottom].X, bottomLeftRoom.Doors[Right].Y));
        corridor.Points.Add(bottomLeftRoom.Doors[Right]);
        corridors.Add(corridor);
    }

    /// <summary>L-образный коридор с поворотом вправо: сначала вниз, потом влево.</summary>
    private static void GenerateRightTurnCorridor(Room topRoom, Room bottomRightRoom, List<Corridor> corridors)
    {
        var corridor = new Corridor { Type = CorridorRightTurn };
        corridor.Points.Add(topRoom.Doors[Bottom]);
        corridor.Points.Add((topRoom.Doors[Bottom].X, bottomRightRoom.Doors[Left].Y));
        corridor.Points.Add(bottomRightRoom.Doors[Left]);
        corridors.Add(corridor);
    }

    /// <summary>Z-образный коридор сверху вниз.</summary>
    private static void GenerateTopToBottomCorridor(Room topRoom, Room bottomRoom, List<Corridor> corridors, Room?[,] grid)
    {
        var corridor = new Corridor { Type = CorridorTopToBottom };
        corridor.Points.Add(topRoom.Doors[Bottom]);
        int minY = topRoom.Doors[Bottom].Y;
        int maxY = bottomRoom.Doors[Top].Y;
        for (int col = 0; col < RoomsPerSide; col++)
        {
            if (grid[topRoom.GridI, col] != null)
                minY = Math.Max(grid[topRoom.GridI, col]!.BottomRight.Y, minY);
            if (grid[bottomRoom.GridI, col] != null)
                maxY = Math.Min(grid[bottomRoom.GridI, col]!.TopLeft.Y, maxY);
        }
        int centerY = RandomGenerator.Next(minY + 1, maxY);
        corridor.Points.Add((topRoom.Doors[Bottom].X, centerY));
        corridor.Points.Add((bottomRoom.Doors[Top].X, centerY));
        corridor.Points.Add(bottomRoom.Doors[Top]);
        corridors.Add(corridor);
    }
}