namespace Text_Roulette.code.Models
{
    public class Shotgun
    {
        private readonly Random generator = new();
        public Stack<bool> chamber = new();
        protected int bullets = 0;
        protected int liveRounds;
        protected int blanks;
        protected int firedRounds;
        public int difficulty = 0;
        protected bool isSawnOff = false;
        public bool isLiveFired;

        public int GetBullets()
        {
            return bullets;
        }

        public void SetBullets(int bullets)
        {
            this.bullets = bullets;
        }

        public void Load()
        {
            blanks = 0;
            liveRounds = 0;

            chamber.Push(true);
            liveRounds++;

            chamber.Push(false);
            blanks++;

            for (int i = 0; i < difficulty; i++)
            {
                if (difficulty > 5) difficulty = 5;

                int type = generator.Next(100);

                if (type < 30)
                {
                    // adjust odds for live round
                    chamber.Push(true);
                    liveRounds++;
                }
                else
                {
                    chamber.Push(false);
                    blanks++;
                }
            }
            _ = chamber.Shuffle();

        }

        public string FireAt(Player target)
        {
            bool result = chamber.Pop();
            if (result && isSawnOff)
            {
                firedRounds++;
                liveRounds--;
                isLiveFired = true;
                target.SetHealth(target.GetHealth() - 2);
                isSawnOff = false;
                return "*BANG!!!";

            }
            else if (result && !isSawnOff)
            {
                firedRounds++;
                liveRounds--;
                isLiveFired = true;
                target.SetHealth(target.GetHealth() - 1);
                isSawnOff = false;
                return "*Bang!";
            }
            else
            {
                isLiveFired = false;
                blanks--;
                isSawnOff = false;
                return "*Click!";
            }
        }

        public string ViewCurrentShell()
        {
            if (chamber.Peek())
            {
                return "Live";
            }
            else
            {
                return "Blank";
            }
        }

        public string Eject()
        {
            if (chamber.Peek())
            {
                chamber.Pop();
                liveRounds--;
                return "Ejects a live round!";
            }
            else
            {
                chamber.Pop();
                blanks--;
                return "Ejects a blank!";
            }
        }

        public bool IsEmpty()
        {
            return chamber.Capacity == 0;
        }
    }


}