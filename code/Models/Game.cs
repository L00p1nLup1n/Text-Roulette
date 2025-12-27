namespace Text_Roulette.code.Models
{
    public class Game
    {
        public Player[] players = new Player[2];
        public int currentPlayer = 0;
        public int rounds;
        string results = "";
        public Shotgun shotgun = new();

        public Stack<Items.itemName> inventory = new();

        public void createPlayers()
        {
            players[0] = new Player(1);
            players[1] = new Player(2);
        }
        public Output Gameplay(Input? input, Output output)
        {

            switch (input?.GetInput())
            {
                case "me":
                    results = shotgun.FireAt(players[currentPlayer]);
                    output.message = results;
                    if (shotgun.isLiveFired)
                    {
                        output.whichPlayerTurn = OtherPlayer();
                        SwitchPlayer();
                    }
                    else
                    {
                        output.whichPlayerTurn = currentPlayer;
                    }
                    output.GunState = Output.GunStateEnum.standard;
                    break;
                case "you":
                    results = shotgun.FireAt(players[OtherPlayer()]);
                    output.message = results;
                    output.whichPlayerTurn = OtherPlayer();
                    SwitchPlayer();
                    output.GunState = Output.GunStateEnum.standard;
                    break;
                case "cmd":
                    results = "Type 'y' to shoot the other player \n'm' to shoot yourself";
                    output.message = results;
                    break;
                case "saw":
                    shotgun.isSawnOff = true;
                    output.GunState = Output.GunStateEnum.sawnOff;
                    results = "The barrel is sawn off, dealing 2 damage for 1 turn!";
                    output.message = results;
                    break;
                case "glass":
                    if (output.GunState == Output.GunStateEnum.sawnOff)
                    {
                        output.GunState = (shotgun.ViewCurrentShell() == "Live") ? Output.GunStateEnum.sawnOffIsLive : Output.GunStateEnum.sawnOffIsBlank;
                    }
                    else output.GunState = (shotgun.ViewCurrentShell() == "Live") ? Output.GunStateEnum.isLive : Output.GunStateEnum.isBlank;
                    results = "Player " + players[currentPlayer].playerID + " peeked inside the chamber";
                    output.message = results;
                    break;

                default:
                    results = "Invalid input!";
                    output.message = results;
                    break;
            }
            output.player1Health = players[0].GetHealth();
            output.player2Health = players[1].GetHealth();


            return output;
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