using Engine;
using SoloTrainGame.Core;
using SoloTrainGame.UI;

namespace SoloTrainGame.GameLogic
{
    public class ChooseActionCardState : IActionState
    {
        public void OnEnterGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClicked.AddListener(CardClicked);
            ServiceLocator.GUIService.SetStateMessage("Choose a card for its action");
        }

        public void OnExitGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClicked.RemoveListener(CardClicked);
            ServiceLocator.GUIService.SetStateMessage(string.Empty);
        }

        public void CardClicked(CardUIObject card)
        {
            GUIServices service = ServiceLocator.GUIService;
            service.CardView.SetCard(card.CardInstance);
            service.CardView.gameObject.SetActive(true);
            service.BackgroundImage.ElementClickedEvent.AddListener(BackgroundClicked);
        }

        private void BackgroundClicked(UIElementClickable element)
        {
            GUIServices service = ServiceLocator.GUIService;
            if (service.CardView.enabled)
            {
                service.CardView.CloseView();
            }
        }

    }
}