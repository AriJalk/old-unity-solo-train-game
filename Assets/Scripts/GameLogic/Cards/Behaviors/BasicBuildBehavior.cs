using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "BasicBuildBehavior", menuName = "ScriptableObjects/Behaviors/BasicBuildBehavior", order = 1)]
    public class BasicBuildBehavior : CardBehaviorSO
    {
        private int _availableMoney;
        public override void StartBehavior(CardSO cardSO)
        {
            _availableMoney = cardSO.GeneratedMoney;
            Debug.Log("Name: " + cardSO.Name + ", Money: " + _availableMoney);
        }
        public override void EndBehavior()
        {
            
        }
    }
}