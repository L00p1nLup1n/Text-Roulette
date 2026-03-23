using Text_Roulette.code.Models.Items;

namespace Text_Roulette.code.Models.Game
{
    public class Game
    {
        public Player[] players = new Player[2];
        public int currentPlayer = 0;
        public int rounds;
        public Shotgun shotgun = new();
        private ShotgunState gunState = ShotgunState.Standard;

        public List<ItemName> inventory = [];



        public void CreatePlayers()
        {
            players[0] = new Player(1);
            players[1] = new Player(2);
        }

        public GameResult Gameplay(string command)
        {
            string message = "";
            string info = "";


            switch (command)
            {
                case "me":
                    message = shotgun.FireAt(players[currentPlayer]);
                    if (shotgun.isLiveFired && !players[OtherPlayer()].handcuffed)
                    {
                        currentPlayer = OtherPlayer();
                    }
                    gunState = ShotgunState.Standard;
                    players[OtherPlayer()].handcuffed = false;
                    break;
                case "you":
                    message = shotgun.FireAt(players[OtherPlayer()]);
                    if (!players[OtherPlayer()].handcuffed)
                        SwitchPlayer();
                    gunState = ShotgunState.Standard;
                    players[OtherPlayer()].handcuffed = false;
                    break;
                case "cmd":
                    info = "Type 'you' to shoot the other player \n'me' to shoot yourself";
                    break;
                case "saw":
                    info = UseItem(ItemName.saw);
                    break;
                case "glass":
                    info = UseItem(ItemName.glass);
                    break;
                case "cig":
                    info = UseItem(ItemName.cig);
                    break;
                case "beer":
                    info = UseItem(ItemName.beer);
                    break;
                case "cuff":
                    info = UseItem(ItemName.cuff);
                    break;
                case "items":
                    if (rounds == 0)
                    {
                        message = "This command is not yet valid...";
                    }
                    else
                    {
                        info = "List of items: " + ViewItems();
                    }
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
                GunState = gunState,
                Info = info,
            };
        }

        private int OtherPlayer()
        {
            return (currentPlayer == 0) ? 1 : 0;
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 0 ? 1 : 0;
        }

        public void GenerateItems()
        {
            Random generator = new();

            // Generate 3 items each call
            for (int i = 0; i < 3; i++)
            {
                if (inventory.Count >= 8) break;
                // int randomResult = generator.Next(5);

                int randomResult = 2;

                switch (randomResult)
                {
                    case 0:
                        inventory.Add(ItemName.saw);
                        break;
                    case 1:
                        inventory.Add(ItemName.glass);
                        break;
                    case 2:
                        inventory.Add(ItemName.cig);
                        break;
                    case 3:
                        inventory.Add(ItemName.beer);
                        break;
                    case 4:
                        inventory.Add(ItemName.cuff);
                        break;
                }
            }
        }

        private string ViewItems()
        {
            string _ = "";
            foreach (ItemName iter in inventory)
            {
                _ += "\n" + iter;
            }
            return _;
        }

        private bool ItemsUsable()
        {
            return rounds >= 1;
        }

        private string UseItem(ItemName itemToBeUsed)
        {
            string _ = "";
            if (ItemsUsable() && !inventory.Contains(itemToBeUsed))
            {
                return "The item requested is not available";
            }

            switch (itemToBeUsed)
            {
                case ItemName.saw:
                    ItemsInfo saw = new(File.ReadAllText("assets/InfoAboutSaw.txt"));
                    if (!ItemsUsable())
                    {
                        _ = saw.Info;
                        break;
                    }
                    if (shotgun.isSawnOff == false)
                    {
                        shotgun.isSawnOff = true;
                        gunState = ShotgunState.SawnOff;
                        inventory.Remove(itemToBeUsed);
                        _ = "The barrel is sawn off, dealing 2 damage for 1 turn!";
                        break;
                    }
                    else
                    {
                        _ = "Shotgun is already sawn off, no op";
                        break;
                    }

                case ItemName.glass:
                    ItemsInfo glass = new(File.ReadAllText("assets/InfoAboutGlass.txt"));

                    if (!ItemsUsable())
                    {
                        _ = glass.Info;
                        break;
                    }
                    if (gunState == ShotgunState.SawnOff)
                    {
                        gunState = (shotgun.ViewCurrentShell() == "Live") ? ShotgunState.SawnOffIsLive : ShotgunState.SawnOffIsBlank;
                    }
                    else
                    {
                        gunState = (shotgun.ViewCurrentShell() == "Live") ? ShotgunState.IsLive : ShotgunState.IsBlank;
                    }
                    _ = $"Player {players[currentPlayer].playerID} peeked inside the chamber";
                    break;

                case ItemName.cig:
                    ItemsInfo cig = new(File.ReadAllText("assets/InfoAboutCig.txt"));
                    if (!ItemsUsable())
                    {
                        _ = cig.Info;
                        break;
                    }
                    players[currentPlayer].Heal(1);
                    _ = "Cigarettes may kill you but a shotgun will do it faster...";
                    break;

                case ItemName.cuff:
                    ItemsInfo cuff = new(File.ReadAllText("assets/InfoAboutHandcuff.txt"));
                    if (!ItemsUsable())
                    {
                        _ = cuff.Info;
                        break;
                    }

                    players[OtherPlayer()].handcuffed = true;
                    _ = $"Player {players[OtherPlayer()].playerID} is handcuffed";
                    break;

                case ItemName.beer:
                    ItemsInfo beer = new(File.ReadAllText("assets/InfoAboutBeer.txt"));

                    if (!ItemsUsable())
                    {
                        _ = beer.Info;
                        break;
                    }

                    _ = shotgun.Eject();
                    break;


                default:
                    return "No op";
            }

            inventory.Remove(itemToBeUsed);
            return _;
        }
    }
}



