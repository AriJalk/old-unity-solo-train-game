using Engine;
using Engine.ResourceManagement;
using HexSystem;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace SoloTrainGame.Core
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
            BuildTestMap();
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

        private void SetTileMaterial(HexTileObject tile)
        {
            if (tile != null)
            {
                Material material = ServiceLocator.MaterialManager.GetColorMaterial(tile.HexData.HexType.TerrainColor);
                if (material != null)
                {
                    tile.MeshRenderer.material = material;
                }  
            }
        }

        public void CreateTile(HexPosition hex, Enums.TerrainType type)
        {
            HexTileObject tile = prefabManager.RetrievePoolObject<HexTileObject>();
            tile.HexData = new MapHexData(hex, ServiceLocator.ScriptableObjectManager.TerrainTypes[type], false, false);
            tile.transform.SetParent(transform);
            tileObjects.Add(tile);
            UpdateTilePosition(tile);
            SetTileMaterial(tile);
        }



        public void BuildTestMap()
        {
            // Build test map
            HexPosition hex = HexPosition.ZERO;
            for (int i = 0; i < 10; i++)
            {
                CreateTile(hex, Enums.TerrainType.Fields);
                HexPosition northHex = HexPosition.GetHexNeighbor(hex, HexPosition.HexDirection.NORTH);
                for (int j = 0; j < 5; j++)
                {
                    CreateTile(northHex, Enums.TerrainType.Urban);
                    northHex = HexPosition.GetHexNeighbor(northHex, HexPosition.HexDirection.NORTH);
                }
                for (int j = 0; j < 5; j++)
                {
                    CreateTile(northHex, Enums.TerrainType.Mountains);
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