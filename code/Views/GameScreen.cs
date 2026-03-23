namespace Text_Roulette.code.Views
{
    using Terminal.Gui;
    using Text_Roulette.code.Models;
    using Text_Roulette.code.Services;

    public class GameScreen
    {
        private readonly GameEngine _engine;
        private readonly AssetLoader _assetLoader = new();

        // UI component references
        private Label? roundInfoLabel;
        private Label? playerStatsLabel;
        private Label? turnInfoLabel;
        private Label? shotgunLabel;
        private Label? messageLabel;
        public Window Window { get; private set; } = null!;

        public GameScreen(GameEngine engine)
        {
            _engine = engine;
            _assetLoader.LoadAll();
        }

        public void RenderState(GameState state)
        {
            if (roundInfoLabel != null)
                roundInfoLabel.Text = $"Round: {state.Round}\nLive Rounds: {state.LiveRounds}\nBlank Rounds: {state.BlankRounds}";

            if (playerStatsLabel != null)
            {
                string p1Hearts = new string('\u2665', Math.Max(0, state.Player1Health));
                string p2Hearts = new string('\u2665', Math.Max(0, state.Player2Health));
                playerStatsLabel.Text = $"Player 1 : {p1Hearts} \nPlayer 2 : {p2Hearts}";
            }

            if (turnInfoLabel != null)
                turnInfoLabel.Text = $"Current Turn: Player {state.CurrentPlayer + 1}";

            if (messageLabel != null)
                messageLabel.Text = state.Message;

            if (shotgunLabel != null)
                shotgunLabel.Text = _assetLoader.GetShotgunArt(state.GunState);


        }

        public Window CreateGameWindow()
        {
            var gameWindow = new Window()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var gameLabel = new Label("Text Roulette")
            {
                X = 1,
                Y = 0
            };
            gameWindow.Add(gameLabel);

            // Round Info Window
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
            roundInfoLabel = new Label("Round: 1\nLive Rounds: 0\nBlank Rounds: 0\nDifficulty: 1")
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

            // Player Stats Window
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
            playerStatsLabel = new Label("Player 1 Health:\nPlayer 2 Health:")
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

            // Turn Info Window
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
            turnInfoLabel = new Label("Current Turn: Player 1")
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

            // Shotgun ASCII Window
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
            var shotgunAscii = new Label("")
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
            shotgunLabel = shotgunAscii;

            // Message Window
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
            messageLabel = new Label("")
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

            // Input label
            var gameInputLabel = new Label("Enter command:")
            {
                X = 1,
                Y = Pos.AnchorEnd(2)
            };
            gameWindow.Add(gameInputLabel);

            // Input text field
            var gameTextField = new TextField("")
            {
                X = Pos.Right(gameInputLabel) + 1,
                Y = Pos.AnchorEnd(2),
                Width = 15,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.White, Color.Black),
                    Focus = new Attribute(Color.BrightYellow, Color.Black),
                }
            };

            // Handle Enter key — call engine directly, render result
            gameTextField.KeyPress += (e) =>
            {
                if (e.KeyEvent.Key == Key.Enter)
                {
                    string input = gameTextField.Text.ToString() ?? "";
                    gameTextField.Text = "";

                    var newState = _engine.ProcessCommand(input);
                    RenderState(newState);

                    e.Handled = true;
                }
            };

            gameWindow.Add(gameTextField);

            // Quit button
            var quitBtn = new Button("Quit")
            {
                X = Pos.Center(),
                Y = Pos.AnchorEnd(1),
                ColorScheme = new ColorScheme()
                {
                    Normal = Application.Driver.MakeAttribute(Color.White, Color.Black),
                    Focus = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black),
                    HotNormal = Application.Driver.MakeAttribute(Color.White, Color.Black),
                    HotFocus = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black)
                }
            };
            quitBtn.Clicked += () =>
            {
                Application.RequestStop();
            };
            gameWindow.Add(quitBtn);

            Window = gameWindow;
            return gameWindow;
        }
    }
}
