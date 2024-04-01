using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "EndRoundBehavior", menuName = "ScriptableObjects/Behaviors/EndRoundBehavior", order = 1)]
    public class EndRoundBehavior : CardBehaviorSO
    {
        public override void StartBehavior(CardSO cardSO)
        {
            Debug.Log("Name: " + cardSO.Name + ", Money: " + cardSO.GeneratedMoney + ", Transport: " + cardSO.GeneratedTransport);
        }

        public override void EndBehavior()
        {
            
        }
    }
}