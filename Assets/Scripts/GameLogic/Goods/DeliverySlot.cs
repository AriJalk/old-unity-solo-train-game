using Engine;

namespace SoloTrainGame.GameLogic
{
    public class DeliverySlot
    {
        public bool IsDelivered { get; private set; }

        public GoodsTypeSO GoodsType { get; private set; }

        public DeliverySlot(Enums.GoodsType goods)
        {
            GoodsType = ServiceLocator.ScriptableObjectManager.GoodsTypes[goods];
        }

        public DeliverySlot(GoodsTypeSO goods)
        {
            GoodsType = goods;
        }

        public bool DeliverGood()
        {
            if (!IsDelivered)
            {
                IsDelivered = true;
                return true;
            }
            return false;
        }

        public bool RemoveGood()
        {
            if (IsDelivered)
            {
                IsDelivered = false;
                return true;
            }
            return false;
        }
    }
}