using Engine;

namespace SoloTrainGame.GameLogic
{
    public class BuildAction : IGameAction
    {
        private BuildingTypeSO _buildingType;
        private HexData _hexTile;
        private IBuilding _building;

        public bool CanExecute { get; private set; }

        public BuildAction(BuildingTypeSO buildingType, float availableMoney, HexData hexTile)
        {
            _hexTile = hexTile;
            _buildingType = buildingType;
            float cost = hexTile.TileType.TerrainCost + buildingType.Cost;
            if (availableMoney >= cost)
            {
                switch (buildingType.BuildingTypeEnum)
                {
                    case Enums.BuildingType.Track:
                        _building = Tracks.BuildTrack(hexTile, buildingType);
                        break;
                    case Enums.BuildingType.Town:
                        _building = Town.BuildTown(hexTile, buildingType);
                        break;
                    case Enums.BuildingType.City:
                        // TODO: produced goods from card choice
                        _building = City.BuildCity(hexTile, buildingType, hexTile.TileType.ProducedGoods);
                        break;
                }
                if (_building != null)
                    _buildingType = buildingType;
                    CanExecute = true;
            }
        }


        public void Execute()
        {
            _hexTile.BuildOnHex(_building);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}