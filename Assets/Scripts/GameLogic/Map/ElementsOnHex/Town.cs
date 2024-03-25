namespace SoloTrainGame.GameLogic
{
    public class Town : SettlementBase
    {
        public Town(ProductionSlot productionSlot) : base(Enums.BuildingType.Town, productionSlot) { }
    }
}