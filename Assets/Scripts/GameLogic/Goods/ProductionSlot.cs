using Engine;

namespace SoloTrainGame.GameLogic
{
    public class ProductionSlot
    {
        public bool IsProduced { get; private set; }

        public GoodsTypeSO GoodsType { get; private set; }

        public ProductionSlot(Enums.GoodsType goods)
        {
            GoodsType = ServiceLocator.ScriptableObjectManager.GoodsTypes[goods];
        }

        public ProductionSlot(GoodsTypeSO goods)
        {
            GoodsType = goods;
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