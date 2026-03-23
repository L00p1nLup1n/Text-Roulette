namespace Text_Roulette.code.Models.Game
{
    public class GameState
    {
        public int CurrentPlayer { get; init; }
        public int Player1Health { get; init; }
        public int Player2Health { get; init; }
        public int Round { get; init; }
        public int Difficulty { get; init; }
        public int LiveRounds { get; init; }
        public int BlankRounds { get; init; }
        public ShotgunState GunState { get; init; } = ShotgunState.Standard;
        public string Message { get; init; } = "";
        public string? LogMessage { get; init; }
        public bool ShotgunReloaded { get; init; }
        public bool GameOver { get; init; }
        public int? Winner { get; init; }
        public string Info { get; init; } = "";
    }
}
