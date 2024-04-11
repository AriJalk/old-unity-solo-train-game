using Engine;
using SoloTrainGame.Core;
using SoloTrainGame.UI;
using System;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    public class TransportState : IActionState
    {
        private GameGUIServices _guiServices;
        public int AvailableTransport { get; private set; }
        public TransportState(int availableTransport)
        {
            AvailableTransport = availableTransport;
            _guiServices = ServiceLocator.GetGUI<GameGUIServices>();
        }

        private void CardClicked(CardUIObject card)
        {
            if (card != null && card.CardInstance.CardData.CanBeDiscarded)
            {
                Debug.Log(card.CardInstance.CardData);
                AddMoney(card.CardInstance.CardData.GeneratedTransport);
                // Discard and move as command
            }
        }

        public void AddMoney(int amount)
        {
            if (amount > 0)
                AvailableTransport += amount;
            _guiServices.SetExtraMessage(AvailableTransport + "T");
        }

        public void RemoveMoney(int amount)
        {
            if (amount > 0)
                AvailableTransport -= amount;
            _guiServices.SetExtraMessage(AvailableTransport + "T");
        }

        public void OnEnterGameState()
        {
            _guiServices.GUIEvents.CardClickedEvent.AddListener(CardClicked);
            _guiServices.SetStateMessage("Select a tile with deliverable cube");
            _guiServices.SetExtraMessage(AvailableTransport + "T");
        }

        public void OnExitGameState()
        {
            _guiServices.GUIEvents.CardClickedEvent.RemoveListener(CardClicked);
        }
    }
}