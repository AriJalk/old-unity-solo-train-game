using Engine;
using SoloTrainGame.Core;
using SoloTrainGame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    public class ChooseActionCardState : IActionState
    {

        private List<CardUIObject> _cards;
        public void OnEnterGameState()
        {
            var guiServices = ServiceLocator.GetGUI<GameGUIServices>();
            if (guiServices != null)
            {
                guiServices.GameGUIEvents.CardClickedEvent?.AddListener(CardClicked);
                guiServices.SetStateMessage("Choose a card for its action");
                guiServices.CardView.PlayActionEvent?.AddListener(PlayAction);
                ServiceLocator.GameEvents.TileSelectedEvent?.AddListener(TestAddRail);
            }
        }

        public void OnExitGameState()
        {
            var guiServices = ServiceLocator.GetGUI<GameGUIServices>();
            if (guiServices != null)
            {
                guiServices.GameGUIEvents.CardClickedEvent?.RemoveListener(CardClicked);
                guiServices.SetStateMessage(string.Empty);
                guiServices.CardView.PlayActionEvent?.RemoveListener(PlayAction);
                ServiceLocator.GameEvents.TileSelectedEvent?.RemoveListener(TestAddRail);
            }
        }


        public void CardClicked(CardUIObject card)
        {
            GameGUIServices service = ServiceLocator.GetGUI<GameGUIServices>();
            if (service != null) 
            {
                service.CardView.SetCard(card.CardInstance);
                service.CardView.gameObject.SetActive(true);
                service.BackgroundImage.ElementClickedEvent.AddListener(BackgroundClicked);
            }     
        }

        private void BackgroundClicked(UIElementClickable element)
        {
            GameGUIServices service = ServiceLocator.GetGUI<GameGUIServices>();
            if (service != null && service.CardView.isActiveAndEnabled)
            {
                service.CardView.CloseView();
            }
        }

        private void PlayAction(CardInstance card)
        {
            card.CardData.CardBehavior.StartBehavior(card.CardData);
            ServiceLocator.GetGUI<GameGUIServices>()?.UIHand.RemoveCardFromHand(card);
        }

        private void TestAddRail(HexTileObject tile)
        {
            if (tile.HexGameData.Tracks == null)
            {
                tile.BuildTracks();
            }
            else if (tile.HexGameData.Tracks != null && !tile.HexGameData.Tracks.IsUpgraded)
            {
                tile.UpgradeTracks();
                Debug.Log("Upgrade");
            }
        }
    }
}