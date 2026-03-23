namespace Text_Roulette.code.Services
{
    using Text_Roulette.code.Models;

    public class AssetLoader
    {
        private readonly Dictionary<GunState, string> _shotgunArt = new();

        public void LoadAll(string assetDir = "assets")
        {
            _shotgunArt[GunState.Standard] = File.ReadAllText(Path.Combine(assetDir, "Shotgun.txt"));
            _shotgunArt[GunState.SawnOff] = File.ReadAllText(Path.Combine(assetDir, "ShotgunSawnOff.txt"));
            _shotgunArt[GunState.IsBlank] = File.ReadAllText(Path.Combine(assetDir, "ShotgunIsBlank.txt"));
            _shotgunArt[GunState.IsLive] = File.ReadAllText(Path.Combine(assetDir, "ShotgunIsLive.txt"));
            _shotgunArt[GunState.SawnOffIsBlank] = File.ReadAllText(Path.Combine(assetDir, "ShotgunSawnOffIsBlank.txt"));
            _shotgunArt[GunState.SawnOffIsLive] = File.ReadAllText(Path.Combine(assetDir, "ShotgunSawnOffIsLive.txt"));
        }

        public string GetShotgunArt(GunState state)
        {
            return _shotgunArt.TryGetValue(state, out var art) ? art : "**Placeholder**";
        }
    }
}
