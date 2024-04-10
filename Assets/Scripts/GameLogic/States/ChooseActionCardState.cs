using Engine;
using SoloTrainGame.Core;
using SoloTrainGame.UI;
using System.Collections.Generic;

namespace SoloTrainGame.GameLogic
{
    public class ChooseActionCardState : IActionState
    {
        private List<CardUIObject> _cards;
        public void OnEnterGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClickedEvent?.AddListener(CardClicked);
            ServiceLocator.GUIService.SetStateMessage("Choose a card for its action");
            ServiceLocator.GUIService.CardView.PlayActionEvent?.AddListener(PlayAction);
        }

        public void OnExitGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClickedEvent.RemoveListener(CardClicked);
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
            if (service.CardView.isActiveAndEnabled)
            {
                service.CardView.CloseView();
            }
        }

        private void PlayAction(CardInstance card)
        {
            card.CardData.CardBehavior.StartBehavior(card.CardData);
        }


    }
}