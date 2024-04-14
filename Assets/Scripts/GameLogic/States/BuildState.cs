using Engine;
using SoloTrainGame.Core;
using SoloTrainGame.UI;
using System;
using Unity.VisualScripting;
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
            if (tile.HexGameData.Tracks == null)
            {
                TestAddRail(tile);
                UpdateState();
            }

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
            _guiServices.GameGUIEvents.CardClickedEvent.AddListener(CardClicked);
            _guiServices.SetStateMessage("Select a tile to build on or discard cards to add their $");
            _guiServices.SetExtraMessage(AvailableMoney + "$");
            ServiceLocator.GameEvents.TileSelectedEvent?.AddListener(TileSelected);
        }

        public void OnExitGameState()
        {
            _guiServices.GameGUIEvents.CardClickedEvent.RemoveListener(CardClicked);
            ServiceLocator.GameEvents.TileSelectedEvent?.RemoveListener(TileSelected);
        }

        private void UpdateState()
        {
            foreach(HexTileObject tile in ServiceLocator.HexGridController.HexTileDictionary.Values)
            {
                if (tile.HexGameData.Tracks == null)
                {
                    foreach(HexTileObject neighbor in tile.Neighbors)
                    {
                        if (neighbor.HexGameData.Tracks != null)
                        {
                            tile.MeshRenderer.AddComponent<TintMeshObject>();
                            break;
                        }
                    }
                }
            }
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