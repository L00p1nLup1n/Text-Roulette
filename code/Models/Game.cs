namespace Text_Roulette.code.Models
{
    public class Game
    {
        public Player[] players = new Player[2];
        public int currentPlayer = 0;
        public int rounds;
        public Shotgun shotgun = new();
        private GunState gunState = GunState.Standard;

        public Stack<Items.itemName> inventory = new();

        public void createPlayers()
        {
            players[0] = new Player(1);
            players[1] = new Player(2);
        }

        public GameResult Gameplay(string command)
        {
            string message;

            switch (command)
            {
                case "me":
                    message = shotgun.FireAt(players[currentPlayer]);
                    if (shotgun.isLiveFired)
                    {
                        currentPlayer = OtherPlayer();
                    }
                    gunState = GunState.Standard;
                    break;
                case "you":
                    message = shotgun.FireAt(players[OtherPlayer()]);
                    SwitchPlayer();
                    gunState = GunState.Standard;
                    break;
                case "cmd":
                    message = "Type 'y' to shoot the other player \n'm' to shoot yourself";
                    break;
                case "saw":
                    shotgun.isSawnOff = true;
                    gunState = GunState.SawnOff;
                    message = "The barrel is sawn off, dealing 2 damage for 1 turn!";
                    break;
                case "glass":
                    if (gunState == GunState.SawnOff)
                    {
                        gunState = (shotgun.ViewCurrentShell() == "Live") ? GunState.SawnOffIsLive : GunState.SawnOffIsBlank;
                    }
                    else
                    {
                        gunState = (shotgun.ViewCurrentShell() == "Live") ? GunState.IsLive : GunState.IsBlank;
                    }
                    message = "Player " + players[currentPlayer].playerID + " peeked inside the chamber";
                    break;
                default:
                    message = "Invalid input!";
                    break;
            }

            return new GameResult
            {
                Message = message,
                CurrentPlayer = currentPlayer,
                Player1Health = players[0].GetHealth(),
                Player2Health = players[1].GetHealth(),
                GunState = gunState
            };
        }

        public int OtherPlayer()
        {
            return (currentPlayer == 0) ? 1 : 0;
        }

        public void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 0 ? 1 : 0;
        }

        public void GenerateItems()
        {
            Random generator = new Random();

            // Generate 3 items each call
            for (int i = 0; i < 3; i++)
            {
                if (inventory.Count >= 8) break;
                int randomResult = generator.Next(5);

                switch (randomResult)
                {
                    case 0:
                        inventory.Push(Items.itemName.beer); break;
                    case 1:
                        inventory.Push(Items.itemName.glass); break;
                    case 2:
                        inventory.Push(Items.itemName.cig); break;
                    case 3:
                        inventory.Push(Items.itemName.saw); break;
                    case 4:
                        inventory.Push(Items.itemName.handcuff); break;
                }
            }
        }

        public void ViewItems()
        {

        }

    }

}
