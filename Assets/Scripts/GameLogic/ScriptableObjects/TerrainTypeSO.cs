
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "TerrainType", menuName = "ScriptableObjects/TerrainType", order = 1)]
    public class TerrainTypeSO : ScriptableObject
    {
        public Enums.TerrainType TerrainType;
        public Enums.GameColor TerrainColor;
        public int TerrainCost;
        public bool CanBuildCity;
    }
}