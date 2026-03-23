namespace Text_Roulette.code.Models.Game
{
    public class GameResult
    {
        public string Message { get; init; } = "";
        public int CurrentPlayer { get; init; }
        public int Player1Health { get; init; }
        public int Player2Health { get; init; }
        public ShotgunState GunState { get; init; } = ShotgunState.Standard;
        public string Info { get; init; } = "";
    }
}
