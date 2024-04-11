using Engine;
using SoloTrainGame.Core;
using SoloTrainGame.UI;
using System;
using UnityEngine;

namespace SoloTrainGame.GameLogic
{
    public class BuildState : IActionState
    {
        private GameGUIServices _guiServices;

        public int AvailableMoney { get; private set; }
        public BuildState(int availableMoney)
        {
            AvailableMoney = availableMoney;
            _guiServices = ServiceLocator.GetGUI<GameGUIServices>();
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
            _guiServices.SetExtraMessage(AvailableMoney + "$");
        }

        public void RemoveMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney -= amount;
            _guiServices.SetExtraMessage(AvailableMoney + "$");
        }

        public void OnEnterGameState()
        {
            _guiServices.GUIEvents.CardClickedEvent.AddListener(CardClicked);
            _guiServices.SetStateMessage("Select a tile to build on or discard cards to add their $");
            _guiServices.SetExtraMessage(AvailableMoney + "$");
            ServiceLocator.GameEvents.TileSelectedEvent?.AddListener(TileSelected);
        }

        public void OnExitGameState()
        {
            _guiServices.GUIEvents.CardClickedEvent.RemoveListener(CardClicked);
            ServiceLocator.GameEvents.TileSelectedEvent?.RemoveListener(TileSelected);
        }
    }
}