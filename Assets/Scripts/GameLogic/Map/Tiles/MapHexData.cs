using HexSystem;

namespace SoloTrainGame.GameLogic
{
    public class MapHexData
    {
        public HexPosition Hex { get; private set; }


        public TerrainTypeSO TileType { get; private set; }


        public Tracks Tracks { get; private set; }

        private SettlementBase _settlement;

        public SettlementBase Settlement
        {
            get { return _settlement; }
            private set { _settlement = value; }
        }



        public MapHexData(HexPosition hex, TerrainTypeSO hexType)
        {
            Hex = hex;
            TileType = hexType;
        }

        public bool BuildRailsOnHex()
        {
            if (Tracks == null)
            {
                Tracks = new Tracks();
                return true;
            }
            return false;
        }

        public bool UpgradeRailsOnHex()
        {
            if (Tracks != null && Tracks.IsUpgraded == false)
            {
                return Tracks.UpgradeTrack();
            }
            return false;
        }

        public bool BuildCityStrict()
        {
            if (Settlement != null && TileType.CanBuildCity == true)
            {
                // TODO: Random delivery from stack
                Settlement = new City(new ProductionSlot(TileType.TerrainColor), new DeliverySlot(TileType.TerrainColor));
                if (Settlement != null)
                    return true;
            }
            return false;
        }

        public bool BuildCityOverride()
        {
            // TODO: Random delivery from stack
            Settlement = new City(new ProductionSlot(TileType.TerrainColor), new DeliverySlot(TileType.TerrainColor));
            if (Settlement != null)
                return true;
            return false;
        }

        public bool BuildTown()
        {
            Settlement = new Town(new ProductionSlot(TileType.TerrainColor));
            if (Settlement != null)
                return true;
            return false;
        }

        public bool UpgradeTownToCity()
        {
            if (Settlement is Town town)
            {
                City city = new City(town.ProductionSlot, new DeliverySlot(TileType.TerrainColor));
                if (city != null) 
                    return true;
            }
            return false;
        }
    }
}