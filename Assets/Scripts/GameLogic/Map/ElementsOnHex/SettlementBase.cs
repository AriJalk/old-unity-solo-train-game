namespace SoloTrainGame.GameLogic
{
    public abstract class SettlementBase : IBuilding
    {
        public BuildingTypeSO BuildingType { get; }
        public GoodsSocket ProductionSocket { get; }
        public HexGameData HexTile { get; }

        public SettlementBase(HexGameData targetHex, BuildingTypeSO buildingType, GoodsSocket productionSlot)
        {
            BuildingType = buildingType;
            ProductionSocket = productionSlot;
            HexTile = targetHex;
        }

    }
}