using Engine;
using HexSystem;
using SoloTrainGame.Core;
using SoloTrainGame.UI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
            if (tile.CanBeClicked)
            {
                TestAddRail(tile);
            }
            Debug.Log(tile.HexGameData.Hex);
            UpdateState();
        }

        public void AddMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney += amount;
            _guiServices.SetExtraMessage(AvailableMoney + "$");
            UpdateState();
        }

        public void RemoveMoney(int amount)
        {
            if (amount > 0)
                AvailableMoney -= amount;
            _guiServices.SetExtraMessage(AvailableMoney + "$");
            UpdateState();
        }

        public void OnEnterGameState()
        {
            _guiServices.GameGUIEvents.CardClickedEvent.AddListener(CardClicked);
            _guiServices.SetStateMessage("Select a tile to build on or discard cards to add their $");
            _guiServices.SetExtraMessage(AvailableMoney + "$");
            ServiceLocator.GameEvents.TileSelectedEvent?.AddListener(TileSelected);
            UpdateState();
        }

        public void OnExitGameState()
        {
            _guiServices.GameGUIEvents.CardClickedEvent.RemoveListener(CardClicked);
            ServiceLocator.GameEvents.TileSelectedEvent?.RemoveListener(TileSelected);
        }

        // TODO: logic is messed up, split 
        private void UpdateState()
        {
            List<HexTileObject> visitedTiles = new List<HexTileObject>();
            foreach(HexTileObject tile in ServiceLocator.HexGridController.HexTileDictionary.Values)
            {
                bool isConnectedToNetwork = false;
                if (tile.HexGameData.Tracks == null || !tile.HexGameData.Tracks.IsUpgraded)
                {
                    foreach(HexTileObject neighbor in tile.Neighbors)
                    {
                        // TODO: not edge case like this with ZERO and more performant (use the list)
                        // also store in bool list 
                        if (neighbor.HexGameData.Tracks != null && tile.MeshRenderer.GetComponent<TintMeshObject>() == null || tile.HexGameData.Hex == Hex.ZERO)
                        {
                            isConnectedToNetwork = true;
                            break;
                        }
                    }
                    if (isConnectedToNetwork)
                    {
                        foreach(BuildingTypeSO buildingType in ServiceLocator.ScriptableObjectManager.BuildingTypes.Values)
                        {
                            if (CostHelper.CalculateBuildCost(buildingType, tile.HexGameData) <= AvailableMoney)
                            {
                                tile.MeshRenderer.AddComponent<TintMeshObject>();
                                tile.CanBeClicked = true;
                                break;
                            }
                        }   
                    }
                }
                else if (tile.MeshRenderer.GetComponent<TintMeshObject>() is TintMeshObject tint)
                {
                    GameObject.Destroy(tint);
                    tile.CanBeClicked = false;
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