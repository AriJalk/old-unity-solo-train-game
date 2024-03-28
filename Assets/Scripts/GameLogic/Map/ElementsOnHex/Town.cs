using Engine;

namespace SoloTrainGame.GameLogic
{
    public class Town : SettlementBase
    {
        private Town(HexData targetHex, BuildingTypeSO buildingType, ProductionSlot productionSlot) : base(targetHex, buildingType, productionSlot) { }

        static public Town BuildTown(HexData hexTile, BuildingTypeSO buildingType)
        {
            if (hexTile != null && hexTile.Settlement == null && hexTile.Tracks != null && hexTile.TileType.CanBuildUpTo >= Enums.BuildingType.Town)
            {
                ProductionSlot slot = new ProductionSlot(hexTile.TileType.ProducedGoods);
                return new Town(hexTile, buildingType, slot);
            }
            return null;
        }
    }
}