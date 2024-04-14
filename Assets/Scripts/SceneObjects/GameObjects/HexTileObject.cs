using Engine;
using HexSystem;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HexTileObject : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private GameObject _trainObject1;
    [SerializeField]
    private GameObject _trainObject2;
    [SerializeField]
    private TownGameObject _townObject;
    [SerializeField]
    private CityGameObject _cityObject;

    public TextMeshPro CostText;

    public Transform CachedTransform { get; private set; }
    // TODO: maybe change to private
    public HexGameData HexGameData { get; set; }

    public List<HexTileObject> Neighbors { get; private set; }

    public bool CanBeClicked { get; set; }

    private void Awake()
    {
        CachedTransform = transform;
        Neighbors = new List<HexTileObject>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CanBeClicked)
        {
            Color color = _meshRenderer.material.color;

        }
    }

    public void Initialize(HexGameData hexData)
    {
        HexGameData = hexData;
        Neighbors = new List<HexTileObject>();
        if (hexData.Tracks != null)
        {
            BuildTracks();
            if (hexData.Tracks.IsUpgraded)
            {
                UpgradeTracks();
            }
        }
        if (hexData.Settlement != null)
        {
            if (hexData.Settlement is Town town)
            {
                BuildTown(town);
            }
            else if (hexData.Settlement is City city)
            {
                BuildCity(city);
            }
        }
        Material material = ServiceLocator.MaterialManager.GetColorMaterial(hexData.TileType.TerrainColor);
        if (material != null)
        {
            _meshRenderer.material = material;
        }
    }

    public void BuildTown(Town town)
    {
        if (HexGameData.BuildOnHex(town))
        {
            _townObject.Initialize(town);
        }
    }

    public void BuildCity(City city)
    {
        if (HexGameData.BuildOnHex(city))
        {
            _cityObject.Initialize(city);
        }
    }

    public void BuildTracks()
    {
        if (HexGameData.BuildOnHex(Tracks.BuildTrack(HexGameData)))
        {
            _trainObject1.gameObject.SetActive(true);
        }
    }

    public void UpgradeTracks()
    {
        if (!HexGameData.Tracks.IsUpgraded)
        {
            if (HexGameData.Tracks.UpgradeTrack())
            {
                _trainObject2.gameObject.SetActive(true);
            }
        }
    }

    public void UpdateState(HexGameData updatedHex)
    {
        if (updatedHex.Settlement != null)
        {

        }
    }


}
