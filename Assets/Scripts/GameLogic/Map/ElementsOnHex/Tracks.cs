namespace SoloTrainGame.GameLogic
{
    public class Tracks : IBuilding
    {
        public bool IsUpgraded { get; private set; }

        public BuildingTypeSO BuildingType { get; }

        public HexData HexTile { get; }

        private Tracks(HexData targetHex, BuildingTypeSO buildingType)
        {
            BuildingType = buildingType;
            HexTile = targetHex;
        }

        static public Tracks BuildTrack(HexData hexTile, BuildingTypeSO buildingType)
        {
            if (hexTile != null && hexTile.Tracks == null && Enums.BuildingType.Track <= hexTile.TileType.CanBuildUpTo)
                return new Tracks(hexTile, buildingType);
            return null;
        }

        public bool UpgradeTrack()
        {
            if (IsUpgraded == false)
            {
                IsUpgraded = true;
            }
            return false;
        }
    }
}