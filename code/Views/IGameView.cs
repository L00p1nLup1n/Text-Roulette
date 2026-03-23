namespace Text_Roulette.code.Views
{
    using Text_Roulette.code.Models.Game;

    public interface IGameView
    {
        void ShowMainMenu(Action onStartGame);
        void RenderGameState(GameState state);
    }
}
