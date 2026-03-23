namespace Text_Roulette.code.Models
{
    public class Player(int playerID)
    {
        public int playerID = playerID;
        protected int health = 5;
        public bool handcuffed = false;

        public int GetHealth()
        {
            return health;
        }

        public void TakeDamage(int damage)
        {
            this.health -= damage;
        }

        public void Heal(int amount)
        {
            this.health += amount;
        }
    }
}