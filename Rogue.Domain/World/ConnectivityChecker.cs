using System.Diagnostics;

namespace Rogue.Domain.World;

/// <summary>
/// Проверка связности графа комнат после генерации.
/// Использует поиск в глубину (DFS) по секторам (0-8).
/// Вызывается только при случайном количестве комнат.
/// </summary>
public static class ConnectivityChecker
{
    /// <summary>Проверить связность всех комнат. Возвращает true, если граф связный.</summary>
    public static bool Check(List<Room> rooms)
    {
        Debug.Assert(rooms.Count >= 3 && rooms.Count <= 9, "Комнат должно быть от 3 до 9");
        // Массив посещённых секторов (индекс = Sector комнаты)
        var visited = new bool[9];
        // Запускаем DFS из первой комнаты и считаем посещённые
        int visitedCount = DepthFirstSearch(rooms[0], visited);
        // Граф связный, если посетили все комнаты
        return visitedCount == rooms.Count;
    }

    /// <summary>Рекурсивный обход в глубину от текущей комнаты.
    /// Возвращает количество посещённых комнат.</summary>
    private static int DepthFirstSearch(Room currentRoom, bool[] visited)
    {
        // Отмечаем текущую комнату как посещённую
        visited[currentRoom.Sector] = true;
        int count = 1;
        // Проверяем все четыре направления
        for (int direction = 0; direction < 4; direction++)
        {
            var neighbor = currentRoom.Connections[direction];
            // Если связь есть и сосед ещё не посещён — идём туда
            if (neighbor != null && !visited[neighbor.Sector])
                count += DepthFirstSearch(neighbor, visited);
        }
        return count;
    }
}