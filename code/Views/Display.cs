namespace Text_Roulette.code.Views
{
    using Text_Roulette.code.Models;

    // This class acts as a facade/wrapper to maintain backward compatibility
    // It delegates all calls to the appropriate specialized classes
    class Display
    {
        // Delegate to GameScreen for input callback
        public static void SetInputCallback(Action callback)
        {
            GameScreen.SetInputCallback(callback);
        }

        // Delegate to MainMenu for game start callback
        public static void SetGameStartCallback(Action callback)
        {
            Views.MainMenu.SetGameStartCallback(callback);
        }

        // Delegate to GameScreen for UI updates
        public static void UpdateRoundInfo(int round, int liveRounds, int blankRounds, int difficulty)
        {
            GameScreen.UpdateRoundInfo(round, liveRounds, blankRounds, difficulty);
        }

        public static void UpdatePlayerStats(int player1Health, int player2Health)
        {
            GameScreen.UpdatePlayerStats(player1Health, player2Health);
        }

        public static void UpdateTurnInfo(int currentPlayer)
        {
            GameScreen.UpdateTurnInfo(currentPlayer);
        }

        public static void UpdateShotgun(string ascii)
        {
            GameScreen.UpdateShotgun(ascii);
        }

        public static void UpdateMessage(string message)
        {
            GameScreen.UpdateMessage(message);
        }

        public static void UpdateOutput(Output output, int round, int difficulty)
        {
            GameScreen.UpdateOutput(output, round, difficulty);
        }

        public static void LogOutput(string message)
        {
            GameScreen.LogOutput(message);
        }

        // Delegate to GameScreen for input access
        public static Input? GetInput()
        {
            return GameScreen.GetInput();
        }

        // Delegate to MainMenu to show the main menu
        public static void MainMenu()
        {
            Views.MainMenu.Show();
        }
    }
}
