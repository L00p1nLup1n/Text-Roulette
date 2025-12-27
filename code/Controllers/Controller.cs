
// {    public class Input
//     {
//         private string input = "";
//         public void SetInput(string input)
//         {
//             this.input = input;
//         }

//         public string GetInput()
//         {
//             return input;
//         }
//     }

//     public class Output
//     {
//         public int whichPlayerTurn;

//         public enum GunStateEnum
//         {
//             blankFired,
//             liveFired,
//             isBlank,
//             isLive,
//             sawnOffIsBlank,
//             sawnOffIsLive
//         }

//         public GunStateEnum GunState;

//         public int player1Health;
//         public int player2Health;
//         public string message = "";

//         public int liveRounds;

//         public int blankRounds;

//     }
namespace Text_Roulette.code.Controllers
{
    using System.Collections;
    using System.Runtime.InteropServices;
    using Text_Roulette.code.Models;
    using Text_Roulette.code.Views;
    public class Controller
    {
        public Output output = new();
        public Input? input = new();
        public Game game = new();
        public StreamReader readerShotgun = new("assets/Shotgun.txt");
        public StreamReader readerShotgunSawnOff = new("assets/ShotgunSawnOff.txt");

        public StreamReader readerShotgunIsBlank = new("assets/ShotgunIsBlank.txt");

        public StreamReader readerShotgunIsLive = new("assets/ShotgunIsLive.txt");

        public StreamReader readerShotgunSawnOffIsBlank = new("assets/ShotgunSawnOffIsBlank.txt");

        public StreamReader readerShotgunSawnOffIsLive = new("assets/ShotgunSawnOffIsLive.txt");

        public string shotgun = "";
        public string shotgunSawnOff = "";
        public string shotgunIsBlank = "";

        public string shotgunIsLive = "";

        public string shotgunSawnOffIsLive = "";

        public string shotgunSawnOffIsBlank = "";

        public void StartGame()
        {
            shotgun = readerShotgun.ReadToEnd();
            shotgunSawnOff = readerShotgunSawnOff.ReadToEnd();
            shotgunIsBlank = readerShotgunIsBlank.ReadToEnd();
            shotgunIsLive = readerShotgunIsLive.ReadToEnd();
            shotgunSawnOffIsBlank = readerShotgunSawnOffIsBlank.ReadToEnd();
            shotgunSawnOffIsLive = readerShotgunSawnOffIsLive.ReadToEnd();

            game.createPlayers();
            output.player1Health = game.players[0].GetHealth();
            output.player2Health = game.players[1].GetHealth();
            output.whichPlayerTurn = 0;
            output.GunState = Output.GunStateEnum.standard;

            game.shotgun.Load();
            output.blankRounds = game.shotgun.blanks;
            output.liveRounds = game.shotgun.liveRounds;

            // Update initial display
            Display.UpdateOutput(output, game.rounds + 1, game.shotgun.difficulty);
            // StreamReader readerShotgun = new("assets/Shotgun.txt");
            Display.UpdateShotgun(shotgun);
        }

        public void ProcessInput()
        {

            input = Display.GetInput();
            if (input == null) return;

            string userInput = input.GetInput();
            if (string.IsNullOrWhiteSpace(userInput)) return;

            // Process gameplay
            output = game.Gameplay(input, output);

            // Update display

            Display.UpdateOutput(output, game.rounds + 1, game.shotgun.difficulty);
            Display.LogOutput($"{output.message}");

            Display.UpdateShotgun(ShotgunRender(output.GunState));

            // Reload shotgun if empty
            if (game.shotgun.IsEmpty())
            {
                game.rounds++;
                game.shotgun.difficulty++;
                game.shotgun.Load();
                output.liveRounds = game.shotgun.liveRounds;
                output.blankRounds = game.shotgun.blanks;
                Display.UpdateRoundInfo(game.rounds + 1, output.liveRounds, output.blankRounds, game.shotgun.difficulty);
                Display.LogOutput("=== New Round Started ===");
            }
        }
        public string ShotgunRender(Output.GunStateEnum state)
        {
            switch (state)
            {
                case Output.GunStateEnum.standard:
                    return shotgun;

                case Output.GunStateEnum.sawnOff:
                    return shotgunSawnOff;

                case Output.GunStateEnum.isBlank:
                    return shotgunIsBlank;

                case Output.GunStateEnum.isLive:
                    return shotgunIsLive;

                case Output.GunStateEnum.sawnOffIsLive:
                    return shotgunSawnOffIsLive;

                case Output.GunStateEnum.sawnOffIsBlank:
                    return shotgunSawnOffIsBlank;
            }
            return "**Placeholder**";

        }
    }
}
