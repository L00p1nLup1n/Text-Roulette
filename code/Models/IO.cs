namespace Text_Roulette.code.Models
{

    public class Input
    {
        private string input = "";
        public void SetInput(string input)
        {
            this.input = input;
        }

        public string GetInput()
        {
            return input;
        }
    }

    public class Output
    {
        public int whichPlayerTurn;

        public enum GunStateEnum
        {
            blankFired,
            liveFired,
            isBlank,
            isLive,
            sawnOffIsBlank,
            sawnOffIsLive
        }
        public GunStateEnum GunState;

        public int player1Health;
        public int player2Health;
        public string message = "";

        public int liveRounds;

        public int blankRounds;
    public void Print()
        {
            string output  = $"Player {whichPlayerTurn}'s turn,\n {player1Health},\n {player2Health},\n {message}, \n{liveRounds}, \n{blankRounds}";
            Console.WriteLine(output);
        }
    }

    
}