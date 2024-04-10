using Engine;
using SoloTrainGame.Core;
using System;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    public class TransportState : IActionState
    {
        public int AvailableTransport { get; private set; }
        public TransportState(int availableTransport)
        {
            AvailableTransport = availableTransport;
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
            ServiceLocator.GUIService.SetExtraMessage(AvailableTransport + "T");
        }

        public void RemoveMoney(int amount)
        {
            if (amount > 0)
                AvailableTransport -= amount;
            ServiceLocator.GUIService.SetExtraMessage(AvailableTransport + "T");
        }

        public void OnEnterGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClickedEvent.AddListener(CardClicked);
            ServiceLocator.GUIService.SetStateMessage("Select a tile with deliverable cube");
            ServiceLocator.GUIService.SetExtraMessage(AvailableTransport + "T");
        }

        public void OnExitGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClickedEvent.RemoveListener(CardClicked);
        }
    }
}