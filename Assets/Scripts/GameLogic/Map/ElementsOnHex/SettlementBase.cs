namespace SoloTrainGame.GameLogic
{
    public abstract class SettlementBase : IBuilding
    {
        public BuildingTypeSO BuildingType { get; }
        public ProductionSlot ProductionSlot { get; }
        public HexData HexTile { get; }

        public SettlementBase(HexData targetHex, BuildingTypeSO buildingType, ProductionSlot productionSlot)
        {
            BuildingType = buildingType;
            ProductionSlot = productionSlot;
            HexTile = targetHex;
        }

    }
}