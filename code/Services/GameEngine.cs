namespace Text_Roulette.code.Services
{
    using Text_Roulette.code.Models.Game;


    public class GameEngine
    {
        private Game game = new();

        public GameState StartNewGame()
        {
            game = new Game();
            game.CreatePlayers();
            game.shotgun.Load();

            return BuildState("", "Game started!");
        }

        public GameState ProcessCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return BuildState("Enter a command.");

            var result = game.Gameplay(command);

            string logMessage = result.Message;
            bool reloaded = false;

            if (game.shotgun.IsEmpty())
            {
                game.rounds++;
                game.shotgun.difficulty++;
                game.GenerateItems();
                game.shotgun.Load();
                reloaded = true;
                if (game.rounds == 1)
                {
                    return BuildState(logMessage, "Items now generated each round! Use 'items' to view.\n");
                }
            }

            return new GameState
            {
                CurrentPlayer = result.CurrentPlayer,
                Player1Health = result.Player1Health,
                Player2Health = result.Player2Health,
                Round = game.rounds + 1,
                Difficulty = game.shotgun.difficulty,
                LiveRounds = game.shotgun.liveRounds,
                BlankRounds = game.shotgun.blanks,
                GunState = result.GunState,
                Message = result.Message,
                LogMessage = logMessage,
                ShotgunReloaded = reloaded,
                GameOver = result.Player1Health <= 0 || result.Player2Health <= 0,
                Winner = result.Player1Health <= 0 ? 2
                       : result.Player2Health <= 0 ? 1
                       : null,
                Info = result.Info
            };
        }

        private GameState BuildState(string message, string info = "")
        {
            return new GameState
            {
                CurrentPlayer = game.currentPlayer,
                Player1Health = game.players[0].GetHealth(),
                Player2Health = game.players[1].GetHealth(),
                Round = game.rounds + 1,
                Difficulty = game.shotgun.difficulty,
                LiveRounds = game.shotgun.liveRounds,
                BlankRounds = game.shotgun.blanks,
                Message = message,
                ShotgunReloaded = false,
                GameOver = false,
                Winner = null,
                Info = info
            };
        }
    }
}
