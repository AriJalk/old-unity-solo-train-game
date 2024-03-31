namespace SoloTrainGame.GameLogic
{
    public abstract class SettlementBase : IBuilding
    {
        public BuildingTypeSO BuildingType { get; }
        public ProductionSlot ProductionSlot { get; }
        public HexGameData HexTile { get; }

        public SettlementBase(HexGameData targetHex, BuildingTypeSO buildingType, ProductionSlot productionSlot)
        {
            BuildingType = buildingType;
            ProductionSlot = productionSlot;
            HexTile = targetHex;
        }

    }
}