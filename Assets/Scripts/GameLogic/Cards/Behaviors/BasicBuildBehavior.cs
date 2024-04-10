using Engine;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    [CreateAssetMenu(fileName = "BasicBuildBehavior", menuName = "ScriptableObjects/Behaviors/BasicBuildBehavior", order = 1)]
    public class BasicBuildBehavior : CardBehaviorSO
    {
        public override void StartBehavior(CardSO cardSO)
        {
            BuildState state = new BuildState(cardSO.GeneratedMoney);
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