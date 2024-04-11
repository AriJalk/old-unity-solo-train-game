using Engine;

namespace SoloTrainGame.GameLogic
{
    public class GoodsCube
    {
        public GoodsTypeSO GoodsType {  get; private set; }

        public GoodsCube(GoodsTypeSO goodsType) 
        {
            GoodsType = goodsType;
        }
    }
}