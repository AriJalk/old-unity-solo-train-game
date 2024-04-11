using Engine;

namespace SoloTrainGame.GameLogic
{
    public class Town : SettlementBase
    {
        private Town(HexGameData targetHex, BuildingTypeSO buildingType, GoodsSocket productionSlot) : base(targetHex, buildingType, productionSlot) { }

        static public Town BuildTown(HexGameData hexTile, BuildingTypeSO buildingType)
        {
            if (hexTile != null && hexTile.Settlement == null && hexTile.Tracks != null && hexTile.TileType.CanBuildUpTo >= Enums.BuildingType.Town)
            {
                GoodsSocket slot = new GoodsSocket(hexTile.TileType.ProducedGoods);
                return new Town(hexTile, buildingType, slot);
            }
            return null;
        }
    }
}