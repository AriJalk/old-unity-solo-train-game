using Engine.ResourceManagement;
using HexSystem;
using System.Collections.Generic;
using UnityEngine;

namespace SoloTrainGame.Core
{
    public class GameManager : MonoBehaviour
    {
        const float TILE_SIZE = 0.5f;

        [SerializeField]
        [Range(0f, 2f)]
        float tileGap = 0.1f;

       
        [SerializeField]
        private Transform prefabStorage;

        private PrefabManager prefabManager;
        private List<HexTileObject> tileObjects;
        private float lastTileGap;



        // Start is called before the first frame update
        void Start()
        {
            prefabManager = new PrefabManager(prefabStorage);
            prefabManager.LoadAndRegisterPrefab<HexTileObject>(PrefabFolder.PREFAB_3D, "HexTile", 30);
            tileObjects = new List<HexTileObject>();
            lastTileGap = tileGap;
            // Build test map
            Hex hex = Hex.ZERO;
            for (int i = 0; i < 50; i++)
            {
                CreateTile(hex);
                Hex northHex = Hex.GetHexNeighbor(hex, Hex.HexDirection.NORTH);
                for(int j = 0; j < 50; j++)
                {
                    CreateTile(northHex);
                    northHex = Hex.GetHexNeighbor(northHex, Hex.HexDirection.NORTH);
                }

                if (i % 2 == 0)
                    hex = Hex.GetHexNeighbor(hex, Hex.HexDirection.NORTH_EAST);
                else
                    hex = Hex.GetHexNeighbor(hex, Hex.HexDirection.SOUTH_EAST);
            }

        }

        // Update is called once per frame
        void Update()
        {
            if(lastTileGap != tileGap)
            {
                lastTileGap = tileGap;
                foreach (HexTileObject tileObj in tileObjects)
                {
                    UpdateTilePosition(tileObj);
                }
            }
        }

        void CreateTile(Hex hex)
        {
            HexTileObject tile = prefabManager.RetrievePoolObject<HexTileObject>();
            tile.HexData = hex;
            tile.transform.SetParent(transform);
            tileObjects.Add(tile);
            UpdateTilePosition(tile);
        }

        void UpdateTilePosition(HexTileObject tile)
        {
            tile.transform.position = Hex.HexToWorld(tile.HexData, TILE_SIZE, tileGap, HexOrientation.FlatLayout);
        }
    }

}