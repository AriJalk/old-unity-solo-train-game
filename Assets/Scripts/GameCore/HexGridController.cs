using Engine;
using Engine.ResourceManagement;
using HexSystem;
using SoloTrainGame.GameLogic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoloTrainGame.Core
{
    public class HexGridController : MonoBehaviour
    {
        [SerializeField]
        const float TILE_SIZE = 0.5f;

        public static Vector3 Center = Vector3.zero;


        // Spacing between tiles
        [SerializeField]
        [Range(0f, 2f)]
        float tileGap = 0.1f;

        private PrefabManager prefabManager;

        // Holds all tiles in the map
        private List<HexTileObject> tileObjects;

        // Last recorded tile gap
        private float lastTileGap;

        Vector3 avaragePosition = Vector3.zero;

        // Bounds
        public static float MinX { get; private set; }
        public static float MaxX { get; private set; }
        public static float MinZ {  get; private set; }
        public static float MaxZ {  get; private set; }

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
            tile.CachedTransform.position = Hex.HexToWorld(tile.HexData.Hex, TILE_SIZE, tileGap, HexOrientation.FlatLayout);
        }

        private void SetTileMaterial(HexTileObject tile)
        {
            if (tile != null)
            {
                Material material = ServiceLocator.MaterialManager.GetColorMaterial(tile.HexData.TileType.TerrainColor);
                if (material != null)
                {
                    tile.MeshRenderer.material = material;
                }
                MeshRenderer mesh = tile.CachedTransform.Find("Town").Find("ProductionSlot").Find("Holder").GetComponent<MeshRenderer>();
                // TODO: Randomize from stack
                mesh.material = ServiceLocator.MaterialManager.GetWoodColorMaterial((Enums.GameColor)UnityEngine.Random.Range(0, 4));
            }
        }

        public void CreateTile(Hex hex, Enums.TerrainType type)
        {
            HexTileObject tile = prefabManager.RetrievePoolObject<HexTileObject>();
            tile.HexData = new HexData(hex, ServiceLocator.ScriptableObjectManager.TerrainTypes[type]);
            tile.CachedTransform.SetParent(transform);
            tileObjects.Add(tile);
            UpdateTilePosition(tile);
            SetTileMaterial(tile);
            UpdateBoundsFromHex(tile);


        }


        private void UpdateBoundsFromHex(HexTileObject hexTile)
        {
            if (hexTile.CachedTransform.position.x < MinX)
                MinX = hexTile.CachedTransform.position.x;
            if (hexTile.CachedTransform.position.x > MaxX)
                MaxX = hexTile.CachedTransform.position.x;
            if (hexTile.CachedTransform.position.z < MinZ)
                MinZ = hexTile.CachedTransform.position.z;
            if (hexTile.CachedTransform.position.z > MaxZ)
                MaxZ = hexTile.CachedTransform.position.z;
            avaragePosition += hexTile.CachedTransform.position;
        }


        public void BuildTestMap()
        {
            // Build test map
            Hex hex = Hex.ZERO;
            for (int i = 0; i < 10; i++)
            {
                CreateTile(hex, Enums.TerrainType.Fields);
                Hex northHex = Hex.GetHexNeighbor(hex, Hex.HexDirection.NORTH);
                for (int j = 0; j < 5; j++)
                {
                    CreateTile(northHex, Enums.TerrainType.Urban);
                    northHex = Hex.GetHexNeighbor(northHex, Hex.HexDirection.NORTH);
                }
                for (int j = 0; j < 5; j++)
                {
                    CreateTile(northHex, Enums.TerrainType.Mountains);
                    northHex = Hex.GetHexNeighbor(northHex, Hex.HexDirection.NORTH);
                }

                if (i % 2 == 0)
                    hex = Hex.GetHexNeighbor(hex, Hex.HexDirection.NORTH_EAST);
                else
                    hex = Hex.GetHexNeighbor(hex, Hex.HexDirection.SOUTH_EAST);
            }
            avaragePosition /= tileObjects.Count;
            Center = avaragePosition;

        }
    }
}