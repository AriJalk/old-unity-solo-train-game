using Engine;
using SoloTrainGame.Core;
using SoloTrainGame.UI;

namespace SoloTrainGame.GameLogic
{
    public class TestNewGameState : IActionState
    {
        public void OnEnterGameState()
        {
            foreach (CardSO card in ServiceLocator.ScriptableObjectManager.CardTypes)
            {
                CardInstance cardInstance = new CardInstance(card);
                ServiceLocator.LogicState.CardHand.Add(cardInstance);
                ServiceLocator.GetGUI<GameGUIServices>().UIHand.AddCardToHandFromInstance(cardInstance);
            }
            ServiceLocator.StateManager.ExitCurrentState();
        }

        public void OnExitGameState()
        {
            ServiceLocator.StateManager.AddState(new ChooseActionCardState());
        }
    }
}