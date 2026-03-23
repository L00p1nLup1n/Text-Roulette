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

    }
}