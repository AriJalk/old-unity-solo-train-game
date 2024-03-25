
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "BuildingType", menuName = "ScriptableObjects/BuildingType", order = 1)]
    public class BuildingTypeSO : ScriptableObject
    {
        public Enums.BuildingType Type;
        public int Cost;
    }
}