using Engine.ResourceManagement;
using HexSystem;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class HexGridController : MonoBehaviour
    {
        [SerializeField]
        const float TILE_SIZE = 0.5f;

        [SerializeField]
        [Range(0f, 2f)]
        float tileGap = 0.1f;

        private PrefabManager prefabManager;
        private List<HexTileObject> tileObjects;
        private float lastTileGap;


        void Start()
        {
            
        }

        void Update()
        {
            if (lastTileGap != tileGap)
            {
                lastTileGap = tileGap;
                foreach (HexTileObject tileObj in tileObjects)
                {
                    UpdateTilePosition(tileObj);
                }
            }
        }

        public void Initialize()
        {
            prefabManager = ServiceLocator.PrefabManager;
            prefabManager.LoadAndRegisterPrefab<HexTileObject>(PrefabFolder.PREFAB_3D, "HexTile", 30);
            tileObjects = new List<HexTileObject>();
            lastTileGap = tileGap;
        }


        private void UpdateTilePosition(HexTileObject tile)
        {
            tile.transform.position = HexPosition.HexToWorld(tile.HexData.Hex, TILE_SIZE, tileGap, HexOrientation.FlatLayout);
        }

        public void CreateTile(HexPosition hex)
        {
            HexTileObject tile = prefabManager.RetrievePoolObject<HexTileObject>();
            tile.HexData = new MapHexData(hex, ServiceLocator.ScriptableObjectManager.TerrainTypes[Enums.TerrainType.Desert], false, false);
            tile.transform.SetParent(transform);
            tileObjects.Add(tile);
            UpdateTilePosition(tile);
        }


        public void BuildTestMap()
        {
            // Build test map
            HexPosition hex = HexPosition.ZERO;
            for (int i = 0; i < 50; i++)
            {
                CreateTile(hex);
                HexPosition northHex = HexPosition.GetHexNeighbor(hex, HexPosition.HexDirection.NORTH);
                for (int j = 0; j < 50; j++)
                {
                    CreateTile(northHex);
                    northHex = HexPosition.GetHexNeighbor(northHex, HexPosition.HexDirection.NORTH);
                }

                if (i % 2 == 0)
                    hex = HexPosition.GetHexNeighbor(hex, HexPosition.HexDirection.NORTH_EAST);
                else
                    hex = HexPosition.GetHexNeighbor(hex, HexPosition.HexDirection.SOUTH_EAST);
            }
        }
    }
}