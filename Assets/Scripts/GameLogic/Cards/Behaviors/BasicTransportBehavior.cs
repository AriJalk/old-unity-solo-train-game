using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "BasicTransportBehavior", menuName = "ScriptableObjects/Behaviors/BasicTransportBehavior", order = 1)]
    public class BasicTransportBehavior : CardBehaviorSO
    {
        private int _availableTransport;
        public override void StartBehavior(CardSO cardSO)
        {
            _availableTransport = cardSO.GeneratedTransport;
            Debug.Log("Name: " + cardSO.Name + ", Transport: " + _availableTransport);
        }
        public override void EndBehavior()
        {
            
        }
    }
}