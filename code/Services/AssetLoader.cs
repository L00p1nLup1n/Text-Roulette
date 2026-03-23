namespace Text_Roulette.code.Services
{
    using Text_Roulette.code.Models;

    public class AssetLoader
    {
        private readonly Dictionary<ShotgunState, string> _shotgunArt = new();

        public void LoadAll(string assetDir = "assets")
        {
            _shotgunArt[ShotgunState.Standard] = File.ReadAllText(Path.Combine(assetDir, "Shotgun.txt"));
            _shotgunArt[ShotgunState.SawnOff] = File.ReadAllText(Path.Combine(assetDir, "ShotgunSawnOff.txt"));
            _shotgunArt[ShotgunState.IsBlank] = File.ReadAllText(Path.Combine(assetDir, "ShotgunIsBlank.txt"));
            _shotgunArt[ShotgunState.IsLive] = File.ReadAllText(Path.Combine(assetDir, "ShotgunIsLive.txt"));
            _shotgunArt[ShotgunState.SawnOffIsBlank] = File.ReadAllText(Path.Combine(assetDir, "ShotgunSawnOffIsBlank.txt"));
            _shotgunArt[ShotgunState.SawnOffIsLive] = File.ReadAllText(Path.Combine(assetDir, "ShotgunSawnOffIsLive.txt"));
        }

        public string GetShotgunArt(ShotgunState state)
        {
            return _shotgunArt.TryGetValue(state, out var art) ? art : "**Placeholder**";
        }
    }
}
