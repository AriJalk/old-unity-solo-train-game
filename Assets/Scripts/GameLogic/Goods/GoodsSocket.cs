using Engine;
using SoloTrainGame.GameLogic;
using System.Collections;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    public class GoodsSocket
    {
        public GoodsCube GoodsCube { get; private set; }

        public GoodsTypeSO GoodsType { get; private set; }

        public GoodsSocket(Enums.GoodsType goods)
        {
            GoodsType = ServiceLocator.ScriptableObjectManager.GoodsTypes[goods];
        }

        public GoodsSocket(GoodsTypeSO goods)
        {
            GoodsType = goods;
        }

        public bool PlaceGood(GoodsCube cube)
        {
            if (GoodsCube == null && cube.GoodsType == GoodsType)
            {
                GoodsCube = cube;
                return true;
            }
            return false;
        }

        public GoodsCube RemoveGood()
        {
            if (GoodsCube != null)
            {
                return GoodsCube;
            }
            return null;
        }
    }
}