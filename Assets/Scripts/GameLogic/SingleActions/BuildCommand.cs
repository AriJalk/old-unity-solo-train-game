using SoloTrainGame.Core;

namespace SoloTrainGame.GameLogic
{
    public class BuildCommand : IGameCommand
    {
        private BuildState _state;
        private BuildingTypeSO _buildingType;
        private HexGameData _hexTile;
        private IBuilding _building;
        private int _paidCost;

        public bool CanExecute { get; private set; }
        public bool IsExecuted { get; private set; }

        public BuildCommand(HexGameData hexTile, BuildingTypeSO buildingType, BuildState buildState)
        {
            _state = buildState;
            _hexTile = hexTile;
            _buildingType = buildingType;
            _paidCost = hexTile.TileType.TerrainCost + buildingType.Cost;
            if (buildState.AvailableMoney >= _paidCost)
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
            if (CanExecute)
            {
                _hexTile.BuildOnHex(_building);
                _state.RemoveMoney(_paidCost);
                IsExecuted = true;
            }
        }

        public void Undo()
        {
            if (IsExecuted)
            {
                _hexTile.RemoveBuildingFromHex(_building);
                _state.AddMoney(_paidCost);
            }
        }
    }
}