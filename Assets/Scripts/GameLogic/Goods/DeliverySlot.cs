namespace SoloTrainGame.GameLogic
{
    public class DeliverySlot
    {
        public bool IsDelivered { get; private set; }

        public Enums.GoodsType GoodsColor { get; private set; }

        public DeliverySlot(Enums.GoodsType color)
        {
            GoodsColor = color;
        }

        public bool DeliverGood()
        {
            if (IsDelivered == false)
            {
                IsDelivered = true;
                return true;
            }
            return false;
        }

        public bool RemoveGood()
        {
            if (IsDelivered == true)
            {
                IsDelivered = false;
                return true;
            }
            return false;
        }
    }
}