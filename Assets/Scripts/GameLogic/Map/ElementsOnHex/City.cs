namespace SoloTrainGame.GameLogic
{
    public class City : SettlementBase
    {
        public GoodsSocket DeliverySocket { get; }

        private City(HexGameData targetHex, BuildingTypeSO buildingType, GoodsSocket productionSlot, GoodsSocket deliverySlot) : 
            base(targetHex, buildingType, productionSlot)
        {
            DeliverySocket = deliverySlot;
        }


        static public City BuildCity(HexGameData hexTile, BuildingTypeSO buildingType, GoodsTypeSO deliveryGoods)
        {
            if (hexTile != null && hexTile.Settlement == null && hexTile.Tracks != null && hexTile.TileType.CanBuildUpTo >= Enums.BuildingType.City)
            {
                GoodsSocket productionSlot = new GoodsSocket(hexTile.TileType.ProducedGoods);
                GoodsSocket deliverySlot = new GoodsSocket(deliveryGoods);
                return new City(hexTile, buildingType, productionSlot, deliverySlot);
            }
            return null;
        }

        public static City UpgradeTownToCity(HexGameData hexTile, BuildingTypeSO buildingType, GoodsSocket deliverySlot)
        {
            if (hexTile.Settlement.BuildingType.BuildingTypeEnum == Enums.BuildingType.Town)
            {
                City city = new City(hexTile, buildingType, hexTile.Settlement.ProductionSocket, deliverySlot);
                return city;
            }
            return null;
        }
    }
}