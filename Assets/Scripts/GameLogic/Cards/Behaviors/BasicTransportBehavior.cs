using Engine;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "BasicTransportBehavior", menuName = "ScriptableObjects/Behaviors/BasicTransportBehavior", order = 1)]
    public class BasicTransportBehavior : CardBehaviorSO
    {
        private int _availableTransport;
        public override void StartBehavior(CardSO cardSO)
        {
            TransportState state = new TransportState(cardSO.GeneratedTransport);
            if (ServiceLocator.StateManager.CurrentState.GetType() == typeof(ChooseActionCardState))
            {
                ServiceLocator.StateManager.ExitCurrentState();
                ServiceLocator.StateManager.AddState(state);
                ServiceLocator.StateManager.EnterNextState();
            }
        }
        public override void EndBehavior()
        {
            
        }
    }
}