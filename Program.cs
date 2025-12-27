namespace Main
{
    using Text_Roulette.code.Views;
    using Text_Roulette.code.Controllers;
    class Program
    {
        static void Main(string[] args)
        {
            // Create controller instance
            Controller controller = new();

            // Set up callback for game start
            Display.SetGameStartCallback(() =>
            {
                controller.StartGame();
            });

            // Set up callback for input processing
            Display.SetInputCallback(() =>
            {
                controller.ProcessInput();
            });

            // Start the UI (this blocks until app exits)
            Display.MainMenu();
        }
        public void welcomeText()
        {
            String msg = "//Welcome to Text Roulette//\n//This game is played with 2 players//\n//There are blanks and live shells in the shotgun, loaded in random order//\n//Type 'y' to shoot the other player, 'm' to shoot yourself//\n//Shooting yourself with a blank will skip the other player's turn//\n//Type 'cmd' to view list of basic commands//\nEnter 1 to play basic mode, no items will be dropped (for players who is unfamiliar with the game)\nEnter 2 to play fun mode, where item are dropped (for pro players)\nEnter q to quit";
            Console.WriteLine(msg);
        }
    }
}