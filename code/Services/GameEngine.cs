namespace Text_Roulette.code.Services
{
    using Text_Roulette.code.Models;

    public class GameEngine
    {
        private Game game = new();

        public GameState StartNewGame()
        {
            game = new Game();
            game.createPlayers();
            game.shotgun.Load();

            return BuildState("Game started!");
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
                game.shotgun.Load();
                reloaded = true;
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
                       : null
            };
        }

        private GameState BuildState(string message)
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
                GunState = GunState.Standard,
                Message = message,
                ShotgunReloaded = false,
                GameOver = false,
                Winner = null
            };
        }
    }
}
