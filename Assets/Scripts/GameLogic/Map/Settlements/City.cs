namespace SoloTrainGame.GameLogic
{
    public class City : SettlementBase
    {
        public DeliverySlot DeliverySlot { get; private set; }
        public City(ProductionSlot productionSlot, DeliverySlot deliverySlot) : base(Enums.BuildingType.City, productionSlot)
        {
            DeliverySlot = deliverySlot;
        }
    }
}