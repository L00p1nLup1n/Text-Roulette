namespace Text_Roulette.code.Models
{
    public class Player
    {
        public List<Items.itemName> inventory = new List<Items.itemName>();
        protected int itemUse = 0;
        public int playerID;
        protected int health = 5;
        protected Boolean handcuffed = false;
        public Player (int playerID) {
            this.playerID = playerID;
        }
        public void SetHealth(int health)
        {
            this.health = health;
        }

        public int GetHealth()
        {
            return this.health;
        }

        // public void GenerateItems()
        // {
        //     Random generator = new Random();

        //     // Generate 3 items each call
        //     for (int i = 0; i < 3; i++)
        //     {
        //         if (inventory.Count >= 8) break;
        //         int randomResult = generator.Next(5);

        //         switch (randomResult)
        //         {
        //             case 0:
        //                 inventory.Add(Items.itemName.beer); break;
        //             case 1:
        //                 inventory.Add(Items.itemName.glass); break;
        //             case 2:
        //                 inventory.Add(Items.itemName.cig); break;
        //             case 3:
        //                 inventory.Add(Items.itemName.saw);
        //                 break;
        //             case 4:
        //                 inventory.Add(Items.itemName.handcuff);
        //                 break;
        //         }


        //     }

        // }
    }
}