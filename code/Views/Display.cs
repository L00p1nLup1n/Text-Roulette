namespace Text_Roulette.code.Views
{
    using System.Security.Cryptography.X509Certificates;
    using Terminal.Gui;

    class Display
    {
        public static void MainMenu()
        {
            Application.Init();
            var top = Application.Top;
            Colors.Base.Normal = new Terminal.Gui.Attribute(Color.White, Color.Black);

            // Create the main window
            var mainWindow = new Window("Text Roulette")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),   // Use the full width of the terminal
                Height = Dim.Fill()   // Use the full height of the terminal
            };


            // Add a label to the main window
            var label = new Label("Welcome to Text Roulette, type 'start' to begin")
            {
                X = Pos.Center(),     // Center the label horizontally
                Y = Pos.Center()      // Center the label vertically
            };
            mainWindow.Add(label);

            var textField = new TextField("")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(label) + 1,
                Width = 50,
            };

            // Create game window (initially not added to top)
            var gameWindow = new Window("Game Started!")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var gameLabel = new Label("Game is now running!")
            {
                X = 1,
                Y = 0
            };
            gameWindow.Add(gameLabel);

            // Add output window for program output
            var outputWindow = new TextView()
            {
                X = 0,
                Y = 2,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 20,
                ReadOnly = true
            };
            outputWindow.Text = "=== Game Output ===\n\n" +
                               "[Player 1 Info]\n\n" +
                               "[Player 2 Info]\n\n" +
                               "[Game Status]\n";
            gameWindow.Add(outputWindow);

            // Add text field for user input in game
            var gameTextField = new TextField("")
            {
                X = 1,
                Y = Pos.AnchorEnd(2),
                Width = Dim.Fill() - 2
            };
            gameWindow.Add(gameTextField);

            // Add quit button at the bottom
            var quitBtn = new Button("Quit")
            {
                X = Pos.Center(),
                Y = Pos.AnchorEnd(1)
            };
            quitBtn.Clicked += () =>
            {
                Application.RequestStop();
            };
            gameWindow.Add(quitBtn);
            // Handle Enter key press in text field
            textField.KeyPress += (e) =>
            {
                if (e.KeyEvent.Key == Key.Enter)
                {
                    if (textField.Text.ToLower() == "start")
                    {
                        Application.Top.Remove(mainWindow);
                        Application.Top.Add(gameWindow);
                        Application.Refresh();
                    }
                    e.Handled = true;
                }
            };

            var btn = new Button("Quit")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(textField) + 1
            };

            mainWindow.Add(btn);
            btn.Clicked += () =>
            {
                Application.RequestStop();
            };
            mainWindow.Add(textField);

            // Add the main window to the application
            Application.Top.Add(mainWindow);

            // Run the application
            Application.Run();

            // Shutdown when done
            Application.Shutdown();
        }
        public void welcomeText()
        {
            String msg = "//Welcome to Text Roulette//\n//This game is played with 2 players//\n//There are blanks and live shells in the shotgun, loaded in random order//\n//Type 'y' to shoot the other player, 'm' to shoot yourself//\n//Shooting yourself with a blank will skip the other player's turn//\n//Type 'cmd' to view list of basic commands//\nEnter 1 to play basic mode, no items will be dropped (for players who is unfamiliar with the game)\nEnter 2 to play fun mode, where item are dropped (for pro players)\nEnter q to quit";
            Console.WriteLine(msg);
        }
    }

}