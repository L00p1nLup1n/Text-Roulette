namespace Text_Roulette.code.Views
{
    using Text_Roulette.code.Models;
    using Text_Roulette.code.Services;

    public class Display : IGameView
    {
        private readonly GameScreen _gameScreen;

        public Display(GameEngine engine)
        {
            _gameScreen = new GameScreen(engine);
        }

        public void ShowMainMenu(Action onStartGame)
        {
            MainMenu.Show(_gameScreen, onStartGame);
        }

        public void RenderGameState(GameState state)
        {
            _gameScreen.RenderState(state);
        }
    }
}
