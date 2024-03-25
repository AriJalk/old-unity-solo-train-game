
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "TerrainType", menuName = "ScriptableObjects/TerrainType", order = 1)]
    public class TerrainTypeSO : ScriptableObject
    {
        public Enums.TerrainType Type;
        public int TerrainCost;
    }
}