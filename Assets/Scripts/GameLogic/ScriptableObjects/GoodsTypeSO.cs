
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "GoodsType", menuName = "ScriptableObjects/GoodsType", order = 1)]
    public class GoodsTypeSO : ScriptableObject
    {
        public Enums.GoodsType GoodsType;
        public Enums.GameColor GoodsColor;
        public int SellValue;
    }
}