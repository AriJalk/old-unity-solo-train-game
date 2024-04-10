using Engine;
using SoloTrainGame.Core;

namespace SoloTrainGame.GameLogic
{
    public class NewGameState : IActionState
    {
        public void OnEnterGameState()
        {
            foreach (CardSO card in ServiceLocator.ScriptableObjectManager.CardTypes)
            {
                CardInstance cardInstance = new CardInstance(card);
                ServiceLocator.LogicState.CardHand.Add(cardInstance);
                ServiceLocator.GUIService.UIHand.AddCardToHandFromInstance(cardInstance);
            }
            ServiceLocator.StateManager.ExitCurrentState();
        }

        public void OnExitGameState()
        {
            ServiceLocator.StateManager.AddState(new ChooseActionCardState());
        }
    }
}