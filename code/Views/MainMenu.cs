namespace Text_Roulette.code.Views
{
    using Terminal.Gui;

    class MainMenu
    {
        public static void Show(GameScreen gameScreen, Action onGameStart)
        {
            Application.Init();
            var top = Application.Top;
            Colors.Base.Normal = new Attribute(Color.White, Color.Black);

            // Create the main window
            var mainWindow = new Window()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
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
                Y = Pos.Center() - 8
            };
            mainWindow.Add(banner);

            var label = new Label("Welcome to Text Roulette, type 'start' to begin")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(banner) + 1
            };
            mainWindow.Add(label);

            var menuInputLabel = new Label("Enter command:")
            {
                X = Pos.Center() - 15,
                Y = Pos.Bottom(label) + 1
            };
            mainWindow.Add(menuInputLabel);

            var textField = new TextField("")
            {
                X = Pos.Right(menuInputLabel) + 1,
                Y = Pos.Bottom(label) + 1,
                Width = 15,
                ColorScheme = new ColorScheme()
                {
                    Normal = new Attribute(Color.White, Color.Black),
                    Focus = new Attribute(Color.BrightYellow, Color.Black),
                }
            };

            // Create About window
            var aboutWindow = new Window("About Text Roulette")
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            string aboutText = "";
            try
            {
                using StreamReader aboutTextReader = new("assets/AboutText.txt");
                aboutText = aboutTextReader.ReadToEnd();
            }
            catch
            {
                aboutText = "About Text Roulette";
            }

            var aboutLabel = new Label(aboutText)
            {
                X = Pos.Center(),
                Y = Pos.Center()
            };
            aboutWindow.Add(aboutLabel);

            aboutWindow.KeyPress += (e) =>
            {
                Application.Top.Remove(aboutWindow);
                Application.Top.Add(mainWindow);
                Application.Refresh();
                e.Handled = true;
            };

            // Create game window
            var gameWindow = gameScreen.CreateGameWindow();

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

                        onGameStart();
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
                Y = Pos.Bottom(textField) + 1,
                ColorScheme = new ColorScheme()
                {
                    Normal = Application.Driver.MakeAttribute(Color.White, Color.Black),
                    Focus = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black),
                    HotNormal = Application.Driver.MakeAttribute(Color.White, Color.Black),
                    HotFocus = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black)
                }
            };

            mainWindow.Add(btn);
            btn.Clicked += () =>
            {
                Application.RequestStop();
            };
            mainWindow.Add(textField);

            Application.Top.Add(mainWindow);
            Application.Run();
            Application.Shutdown();
        }
    }
}
