namespace SoloTrainGame.GameLogic
{
    public class Tracks : IBuilding
    {
        public bool IsUpgraded { get; private set; }

        public BuildingTypeSO BuildingType { get; }

        public HexGameData HexTile { get; }

        private Tracks(HexGameData targetHex, BuildingTypeSO buildingType)
        {
            BuildingType = buildingType;
            HexTile = targetHex;
        }

        static public Tracks BuildTrack(HexGameData hexTile, BuildingTypeSO buildingType)
        {
            if (hexTile != null && hexTile.Tracks == null && Enums.BuildingType.Track <= hexTile.TileType.CanBuildUpTo)
                return new Tracks(hexTile, buildingType);
            return null;
        }

        public bool UpgradeTrack()
        {
            if (!IsUpgraded)
            {
                IsUpgraded = true;
            }
            return false;
        }
    }
}