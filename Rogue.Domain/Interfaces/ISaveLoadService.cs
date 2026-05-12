using Rogue.Domain.Game;

namespace Rogue.Domain.Interfaces;

/// <summary>
/// Сервис сохранения и загрузки игры. Реализуется Rogue.Data.
/// </summary>
public interface ISaveLoadService
{
    /// <summary>Сохранить состояние игры.</summary>
    void Save(GameState gameState);

    /// <summary>Загрузить состояние игры. Возвращает null, если сохранения нет.</summary>
    GameState? Load();

    /// <summary>Сохранить таблицу лидеров.</summary>
    void SaveLeaderboard(GameStatistics statistics);

    /// <summary>Загрузить таблицу лидеров.</summary>
    List<GameStatistics> LoadLeaderboard();
}