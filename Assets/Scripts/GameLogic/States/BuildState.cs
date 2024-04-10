using Engine;
using SoloTrainGame.Core;
using System;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    public class BuildState : IActionState
    {
        public int AvailableMoney { get; private set; }
        public BuildState(int availableMoney)
        {
            AvailableMoney = availableMoney;
        }

        private void CardClicked(CardUIObject card)
        {
            if (card != null)
            {
                Debug.Log(card.CardInstance.CardData);
            }
        }

        public void AddMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney += amount;
        }

        public void RemoveMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney -= amount;
        }

        public void OnEnterGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClickedEvent.AddListener(CardClicked);
        }

        public void OnExitGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClickedEvent.RemoveListener(CardClicked);
        }
    }
}