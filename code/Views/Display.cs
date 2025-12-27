namespace Text_Roulette.code.Views
{
    using System.Security.Cryptography.X509Certificates;
    using Terminal.Gui;
    using Text_Roulette.code.Models;

    class Display
    {
        // UI component references
        private static Label? roundInfoLabel;
        private static Label? playerStatsLabel;
        private static Label? turnInfoLabel;
        private static Label? shotgunLabel;
        private static Label? messageLabel;
        private static TextView? outputWindow;
        private static Input? inputHandler;
        private static Action? onInputReceived;
        private static Action? onGameStart;

        // Set callback for when input is received
        public static void SetInputCallback(Action callback)
        {
            onInputReceived = callback;
        }

        // Set callback for when game starts
        public static void SetGameStartCallback(Action callback)
        {
            onGameStart = callback;
        }

        // UI update methods - API for Controller to use
        public static void UpdateRoundInfo(int round, int liveRounds, int blankRounds, int difficulty)
        {
            if (roundInfoLabel != null)
                roundInfoLabel.Text = $"Round: {round}\nLive Rounds: {liveRounds}\nBlank Rounds: {blankRounds}";
        }

        public static void UpdatePlayerStats(int player1Health, int player2Health)
        {
            if (playerStatsLabel != null)
            {
                string p1Hearts = new string('♥', player1Health);
                string p2Hearts = new string('♥', player2Health);
                playerStatsLabel.Text = $"Player 1 : {p1Hearts} \nPlayer 2 : {p2Hearts}";
            }
        }

        public static void UpdateTurnInfo(int currentPlayer)
        {
            if (turnInfoLabel != null)
                turnInfoLabel.Text = $"Current Turn: Player {currentPlayer + 1}";
        }

        public static void UpdateShotgun(string ascii)
        {
            if (shotgunLabel != null)
                shotgunLabel.Text = ascii;
        }

        public static void UpdateMessage(string message)
        {
            if (messageLabel != null)
                messageLabel.Text = message;
        }

        public static void UpdateOutput(Output output, int round, int difficulty)
        {
            UpdateRoundInfo(round, output.liveRounds, output.blankRounds, difficulty);
            UpdatePlayerStats(output.player1Health, output.player2Health);
            UpdateTurnInfo(output.whichPlayerTurn);
            UpdateMessage(output.message);
        }

        public static void LogOutput(string message)
        {
            if (outputWindow != null)
                outputWindow.Text += message + "\n";
        }

        // API for Controller to access input
        public static Input? GetInput()
        {
            return inputHandler;
        }

        public static void MainMenu()
        {
            Application.Init();
            var top = Application.Top;
            Colors.Base.Normal = new Attribute(Color.White, Color.Black);

            // Create the main window


            var mainWindow = new Window("Text Roulette")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),   // Use the full width of the terminal
                Height = Dim.Fill()   // Use the full height of the terminal
            };

            // Load and display banner
            string logo = "";
            try
            {
                using StreamReader reader = new("assets/Banner.txt");
                logo = reader.ReadToEnd();
            }
            catch
            {
                logo = "Text Roulette"; // Fallback if file not found
            }

            var banner = new Label(logo)
            {
                X = Pos.Center(),
                Y = 10
            };
            mainWindow.Add(banner);

            // Add a label to the main window
            var label = new Label("Welcome to Text Roulette, type 'start' to begin")
            {
                X = Pos.Center(),     // Center the label horizontally
                Y = Pos.Bottom(banner) + 2
            };
            mainWindow.Add(label);

            var textField = new TextField("")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(label) + 1,
                Width = 50,
            };

            // Create About window
            var aboutWindow = new Window("About Text Roulette")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            string aboutText = @"Welcome to Text Roulette
This game is played with 2 players
There are blanks and live shells in the shotgun, loaded in random order
Type 'me' to shoot yourself, 'you' to shoot the other player
Shooting yourself with a blank will skip the other player's turn

Press any key to return to main menu...";

            var aboutLabel = new Label(aboutText)
            {
                X = Pos.Center(),
                Y = Pos.Center()
            };
            aboutWindow.Add(aboutLabel);

            // Handle key press to exit about window
            aboutWindow.KeyPress += (e) =>
            {
                Application.Top.Remove(aboutWindow);
                Application.Top.Add(mainWindow);
                Application.Refresh();
                e.Handled = true;
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

            // Create fixed-size windows for game information

            // Round Info Window (top)
            var roundInfoWindow = new FrameView("Round Info")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = 6,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightCyan, Color.Black),
                    Focus = new Attribute(Color.BrightCyan, Color.Black)
                }
            };
            var roundInfoLabel = new Label("Round: 1\nLive Rounds: 0\nBlank Rounds: 0\nDifficulty: 1")
            {
                X = 1,
                Y = 0,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightCyan, Color.Black)
                }
            };
            roundInfoWindow.Add(roundInfoLabel);
            gameWindow.Add(roundInfoWindow);

            // Store reference to static field
            Display.roundInfoLabel = roundInfoLabel;

            // Player Stats Window (below Round Info)
            var playerStatsWindow = new FrameView("Player Stats")
            {
                X = 0,
                Y = 7,
                Width = Dim.Fill(),
                Height = 5,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightGreen, Color.Black),
                    Focus = new Attribute(Color.BrightGreen, Color.Black)
                }
            };
            // var playerStatsLabel = new Label("Player 1 Health: ♥♥♥♥♥ (5)\nPlayer 2 Health: ♥♥♥♥♥ (5)")
            var playerStatsLabel = new Label("Player 1 Health:\nPlayer 2 Health:")
            {
                X = 1,
                Y = 0,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightGreen, Color.Black)
                }
            };
            playerStatsWindow.Add(playerStatsLabel);
            gameWindow.Add(playerStatsWindow);

            // Store reference to static field
            Display.playerStatsLabel = playerStatsLabel;

            // Turn Info Window (below Player Stats)
            var turnInfoWindow = new FrameView("Turn Info")
            {
                X = 0,
                Y = 12,
                Width = Dim.Fill(),
                Height = 4,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightYellow, Color.Black),
                    Focus = new Attribute(Color.BrightYellow, Color.Black)
                }
            };
            var turnInfoLabel = new Label("Current Turn: Player 1")
            {
                X = 1,
                Y = 0,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightYellow, Color.Black)
                }
            };
            turnInfoWindow.Add(turnInfoLabel);
            gameWindow.Add(turnInfoWindow);

            // Store reference to static field
            Display.turnInfoLabel = turnInfoLabel;

            // Shotgun ASCII Window (below Turn Info)
            var shotgunWindow = new FrameView("Shotgun")
            {
                X = 0,
                Y = 16,
                Width = Dim.Fill(),
                Height = 8,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightMagenta, Color.Black),
                    Focus = new Attribute(Color.BrightMagenta, Color.Black)
                }
            };

            string shotgun = "";
            
            //             var shotgunAscii = new Label(@"
            //     ___
            //    /   \_______________________________________________
            //   |    |                                              \
            //   |____|________________________________________________)
            //         \_______________________________________________/
            // ")       
            var shotgunAscii = new Label(shotgun)
            {
                X = 1,
                Y = 0,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightMagenta, Color.Black)
                }
            };
            shotgunWindow.Add(shotgunAscii);
            gameWindow.Add(shotgunWindow);

            // Store reference to static field
            Display.shotgunLabel = shotgunAscii;

            // Message Window (below Shotgun)
            var messageWindow = new FrameView("Message")
            {
                X = 0,
                Y = 24,
                Width = Dim.Fill(),
                Height = 4,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightRed, Color.Black),
                    Focus = new Attribute(Color.BrightRed, Color.Black)
                }
            };
            var messageLabel = new Label("")
            {
                X = 1,
                Y = 0,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.BrightRed, Color.Black)
                }
            };
            messageWindow.Add(messageLabel);
            gameWindow.Add(messageWindow);

            // Store reference to static field
            Display.messageLabel = messageLabel;

            // Add output window for program output (below message window)
            var outputWindow = new TextView()
            {
                X = 0,
                Y = 28,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 31,
                ReadOnly = true,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.White, Color.Black)
                }
            };
            outputWindow.Text = "=== Game Output ===\n";
            gameWindow.Add(outputWindow);

            // Store reference to static field
            Display.outputWindow = outputWindow;

            // Create input handler
            var inputHandler = new Input();

            // Store reference to static field
            Display.inputHandler = inputHandler;

            // Add label for game input
            var gameInputLabel = new Label("Enter command:")
            {
                X = 1,
                Y = Pos.AnchorEnd(3)
            };
            gameWindow.Add(gameInputLabel);

            // Add text field for user input in game
            var gameTextField = new TextField("")
            {
                X = 1,
                Y = Pos.AnchorEnd(2),
                Width = Dim.Fill() - 2
            };

            // Handle Enter key press in game text field
            gameTextField.KeyPress += (e) =>
            {
                if (e.KeyEvent.Key == Key.Enter)
                {
                    // Only capture and store input
                    inputHandler.SetInput(gameTextField.Text.ToString() ?? "");

                    // Log to output window
                    outputWindow.Text += $"Input: {gameTextField.Text}\n";

                    // Clear text field
                    gameTextField.Text = "";

                    // Notify controller that input was received
                    onInputReceived?.Invoke();

                    e.Handled = true;
                }
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

                        // Start the game
                        onGameStart?.Invoke();
                    }
                    else if (textField.Text.ToLower() == "about")
                    {
                        textField.Text = "";
                        Application.Top.Remove(mainWindow);
                        Application.Top.Add(aboutWindow);
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
    }

}