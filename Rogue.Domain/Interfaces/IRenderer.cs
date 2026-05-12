using Rogue.Domain.Game;

namespace Rogue.Domain.Interfaces;

/// <summary>
/// Отрисовщик игры. Реализуется Presentation-слоем.
/// </summary>
public interface IRenderer
{
    /// <summary>Отрисовать главный меню.</summary>
    void RenderMainMenu();

    /// <summary>Отрисовать экран игры.</summary>
    void RenderGameScreen(GameState gameState);

    /// <summary>Отрисовать таблицу лидеров.</summary>
    void RenderLeaderboard();
}