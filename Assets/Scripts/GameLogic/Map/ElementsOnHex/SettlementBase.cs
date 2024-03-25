namespace SoloTrainGame.GameLogic
{
    public abstract class SettlementBase
    {
        public Enums.BuildingType SettlementType { get; private set; }
        public ProductionSlot ProductionSlot { get; protected set; }

        public SettlementBase(Enums.BuildingType settlementType, ProductionSlot productionSlot)
        {
            SettlementType = settlementType;
            ProductionSlot = productionSlot;
        }

        public static City UpgradeTownToCity(Town town, DeliverySlot deliverySlot)
        {
            City city = new City(town.ProductionSlot, deliverySlot);
            return city;
        }
    }
}