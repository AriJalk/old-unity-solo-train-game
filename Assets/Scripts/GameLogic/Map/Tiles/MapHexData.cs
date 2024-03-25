using HexSystem;

namespace SoloTrainGame.GameLogic
{
    public class MapHexData
    {
        private HexPosition _hex;

        public HexPosition Hex
        {
            get { return _hex; }
            private set { _hex = value; }
        }

        private TerrainTypeSO _hexType;

        public TerrainTypeSO HexType
        {
            get { return _hexType; }
            set { _hexType = value; }
        }

        readonly private bool _isTrackContaintsRiver;
        public bool IsTrackContaintsRiver
        {
            get
            {
                return _isTrackContaintsRiver;
            }
        }

        public bool CanCityBeBuilt { get; private set; }

        public Tracks Tracks {  get; private set; }

        private SettlementBase _settlement;

        public SettlementBase Settlement
        {
            get { return _settlement; }
            private set { _settlement = value; }
        }



        public MapHexData(HexPosition hex, TerrainTypeSO hexType, bool isRiver, bool canCityBeBuilt)
        {
            _hex = hex;
            _hexType = hexType;
            _isTrackContaintsRiver = isRiver;
            CanCityBeBuilt = canCityBeBuilt;
        }

        public bool BuildRailsOnHex()
        {
            if(Tracks == null)
            {
                Tracks = new Tracks();
                return true;
            }
            return false;        
        }

        public bool UpgradeRailsOnHex()
        {
            if(Tracks != null && Tracks.IsUpgraded == false)
            {
                return Tracks.UpgradeTrack();
            }
            return false;
        }
    }
}