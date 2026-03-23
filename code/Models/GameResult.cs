namespace Text_Roulette.code.Models
{
    public class GameResult
    {
        public string Message { get; init; } = "";
        public int CurrentPlayer { get; init; }
        public int Player1Health { get; init; }
        public int Player2Health { get; init; }
        public GunState GunState { get; init; } = GunState.Standard;
    }
}
