using HexSystem;
using UnityEngine;


namespace SoloTrainGame.GameLogic
{
    public class HexGameData
    {
        public Hex Hex { get; private set; }


        public TerrainTypeSO TileType { get; private set; }


        public Tracks Tracks { get; private set; }

        private SettlementBase _settlement;

        public SettlementBase Settlement
        {
            get { return _settlement; }
            private set { _settlement = value; }
        }



        public HexGameData(Hex hex, TerrainTypeSO hexType)
        {
            Hex = hex;
            TileType = hexType;
        }

        public bool BuildOnHex(IBuilding building)
        {
            Debug.Log(building.HexTile.Hex.Equals(this.Hex));
            if (building != null && building.HexTile.Hex.Equals(this.Hex))
            {
                switch (building.BuildingType.BuildingTypeEnum)
                {
                    case Enums.BuildingType.Track:
                        Tracks = building as Tracks;
                        break;
                    case Enums.BuildingType.Town:
                    case Enums.BuildingType.City:
                        Settlement = building as SettlementBase; 
                        break;
                    default:
                        return false;
                }
                return true;
            }
            return false;
        }

        public bool RemoveBuildingFromHex(IBuilding building)
        {
            if (building != null && building.HexTile.Equals(this))
            {
                switch (building.BuildingType.BuildingTypeEnum)
                {
                    case Enums.BuildingType.Track:
                        Tracks = null;
                        break;
                    case Enums.BuildingType.Town:
                    case Enums.BuildingType.City:
                        Settlement = null;
                        break;
                    default:
                        return false;
                }
                return true;
            }
            return false;
        }

        public bool UpgradeRailsOnHex()
        {
            if (Tracks != null && !Tracks.IsUpgraded)
            {
                return Tracks.UpgradeTrack();
            }
            return false;
        }

        public override string ToString()
        {
            string str = "Hex: " + Hex.Position + "\n";
            str += "Type: " + TileType.TerrainType;
            return str;
        }
    }
}