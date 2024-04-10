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
            if (card != null && card.CardInstance.CardData.CanBeDiscarded)
            {
                Debug.Log(card.CardInstance.CardData);
                AddMoney(card.CardInstance.CardData.GeneratedMoney);
                // Discard and move as command
            }
        }

        private void TileSelected(HexTileObject tile)
        {
            Debug.Log(tile.HexGameData.Hex.Position);
        }

        public void AddMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney += amount;
            ServiceLocator.GUIService.SetExtraMessage(AvailableMoney + "$");
        }

        public void RemoveMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney -= amount;
            ServiceLocator.GUIService.SetExtraMessage(AvailableMoney + "$");
        }

        public void OnEnterGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClickedEvent.AddListener(CardClicked);
            ServiceLocator.GUIService.SetStateMessage("Select a tile to build on or discard cards to add their $");
            ServiceLocator.GUIService.SetExtraMessage(AvailableMoney + "$");
            ServiceLocator.GameEvents.TileSelectedEvent?.AddListener(TileSelected);
        }

        public void OnExitGameState()
        {
            ServiceLocator.GUIService.GUIEvents.CardClickedEvent.RemoveListener(CardClicked);
            ServiceLocator.GameEvents.TileSelectedEvent?.RemoveListener(TileSelected);
        }
    }
}