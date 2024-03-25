namespace SoloTrainGame.GameLogic
{
    public class ProductionSlot
    {
        public bool IsProduced { get; private set; }

        public Enums.GameColor GoodsType { get; private set; }

        public ProductionSlot(Enums.GameColor color)
        {
            GoodsType = color;
        }

        public bool ProduceGood()
        {
            if (IsProduced == false)
            {
                IsProduced = true;
                return true;
            }
            return false;
        }

        public bool RemoveGood()
        {
            if (IsProduced == true)
            {
                IsProduced = false;
                return true;
            }
            return false;
        }
    }
}