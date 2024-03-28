
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "BuildingType", menuName = "ScriptableObjects/BuildingType", order = 1)]
    public class BuildingTypeSO : ScriptableObject
    {
        public Enums.BuildingType BuildingTypeEnum;
        public Enums.BuildingType Prerequisite;
        public int Cost;
    }
}