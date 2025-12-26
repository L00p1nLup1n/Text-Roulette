namespace Controllers.code.GameLogic
{
    using Text_Roulette.code.Models;
    public class GameLogic
    {
        public Player[] players = new Player[2];
        public int currentPlayer = 0;
        public int rounds;
        string results = "";
        string chamberResults = "";

        public Shotgun shotgun = new();

        public Stack<Items.itemName> inventory = new();

        // public void Gameplay(IInput input, Ouptut output)
        // {

        //     switch (input.GetInput())
        //     {
        //         case "me":
        //             results = shotgun.FireAt(players[currentPlayer]);
        //             if (shotgun.isLiveFired) SwitchPlayer();
        //             break;
        //         case "you":
        //             results = shotgun.FireAt(players[OtherPlayer()]);
        //             break;

        //     }
        // }

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