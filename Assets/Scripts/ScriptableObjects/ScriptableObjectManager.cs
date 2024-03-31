using SoloTrainGame.GameLogic;
using System;
using System.Collections.Generic;
using UnityEngine;


public class ScriptableObjectManager
{
    public Dictionary<Enums.BuildingType, BuildingTypeSO> BuildingTypes { get; private set; }
    public Dictionary<Enums.GoodsType, GoodsTypeSO> GoodsTypes { get; private set; }
    public Dictionary<Enums.TerrainType, TerrainTypeSO> TerrainTypes { get; private set; }
    public List<CardSO> CardTypes { get; private set; }

    public ScriptableObjectManager()
    {
        BuildingTypes = new Dictionary<Enums.BuildingType, BuildingTypeSO>();
        GoodsTypes = new Dictionary<Enums.GoodsType, GoodsTypeSO>();
        TerrainTypes = new Dictionary<Enums.TerrainType, TerrainTypeSO>();
        CardTypes = new List<CardSO>();
        LoadBuildingTypes();
        LoadGoodsTypes();
        LoadTerrainTypes();
        LoadCardTypes();

    }

    private void LoadBuildingTypes()
    {
        foreach (Enums.BuildingType type in Enum.GetValues(typeof(Enums.BuildingType)))
        {
            string path = "ScriptableObjects/BuildingType/" + type;
            Debug.Log("PATH: " + path);
            BuildingTypeSO buildingTypeSO = Resources.Load<BuildingTypeSO>(path);
            if (buildingTypeSO != null)
            {
                BuildingTypes.Add(type, buildingTypeSO);
            }
            else
            {
                Debug.LogError("Cant load: " + type);
            }
        }
    }

    private void LoadGoodsTypes()
    {
        foreach (Enums.GoodsType type in Enum.GetValues(typeof(Enums.GoodsType)))
        {
            string path = "ScriptableObjects/GoodsType/" + type;
            Debug.Log("PATH: " + path);
            GoodsTypeSO goodsTypeSO = Resources.Load<GoodsTypeSO>(path);
            if (goodsTypeSO != null)
            {
                GoodsTypes.Add(type, goodsTypeSO);
            }
            else
            {
                Debug.LogError("Cant load: " + type);
            }
        }
    }

    public void LoadTerrainTypes()
    {
        foreach (Enums.TerrainType type in Enum.GetValues(typeof(Enums.TerrainType)))
        {
            string path = "ScriptableObjects/TerrainType/" + type;
            Debug.Log("PATH: " + path);
            TerrainTypeSO terrainTypeSO = Resources.Load<TerrainTypeSO>(path);
            if (terrainTypeSO != null)
            {
                TerrainTypes.Add(type, terrainTypeSO);
            }
            else
            {
                Debug.LogError("Cant load: " + type);
            }
        }
    }

    public void LoadCardTypes()
    {
        string path = "ScriptableObjects/CardType/";
        CardSO[] cards = Resources.LoadAll<CardSO>(path);
        foreach (CardSO card in cards)
        {
            CardTypes.Add(card);
        }
    }

}