namespace Main
{
    using Text_Roulette.code.Views;
    using Text_Roulette.code.Services;

    class Program
    {
        static void Main(string[] args)
        {
            var engine = new GameEngine();
            var view = new Display(engine);

            view.ShowMainMenu(() =>
            {
                var state = engine.StartNewGame();
                view.RenderGameState(state);
            });
        }
    }
}
