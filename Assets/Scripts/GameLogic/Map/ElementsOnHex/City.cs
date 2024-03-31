namespace SoloTrainGame.GameLogic
{
    public class City : SettlementBase
    {
        public DeliverySlot DeliverySlot { get; }

        private City(HexGameData targetHex, BuildingTypeSO buildingType, ProductionSlot productionSlot, DeliverySlot deliverySlot) : 
            base(targetHex, buildingType, productionSlot)
        {
            DeliverySlot = deliverySlot;
        }


        static public City BuildCity(HexGameData hexTile, BuildingTypeSO buildingType, GoodsTypeSO deliveryGoods)
        {
            if (hexTile != null && hexTile.Settlement == null && hexTile.Tracks != null && hexTile.TileType.CanBuildUpTo >= Enums.BuildingType.City)
            {
                ProductionSlot productionSlot = new ProductionSlot(hexTile.TileType.ProducedGoods);
                DeliverySlot deliverySlot = new DeliverySlot(deliveryGoods);
                return new City(hexTile, buildingType, productionSlot, deliverySlot);
            }
            return null;
        }

        public static City UpgradeTownToCity(HexGameData hexTile, BuildingTypeSO buildingType, DeliverySlot deliverySlot)
        {
            if (hexTile.Settlement.BuildingType.BuildingTypeEnum == Enums.BuildingType.Town)
            {
                City city = new City(hexTile, buildingType, hexTile.Settlement.ProductionSlot, deliverySlot);
                return city;
            }
            return null;
        }
    }
}