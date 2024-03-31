using HexSystem;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using UnityEngine;

public class HexTileObject : MonoBehaviour
{
    public Transform CachedTransform { get; private set; }
    public HexGameData HexGameData { get; set; }

    public List<HexTileObject> Neighbors { get; private set; }

    public MeshRenderer MeshRenderer;

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

    }

    public void Initialize(HexGameData hexData)
    {
        HexGameData = hexData;
        Neighbors = new List<HexTileObject>();
    }
}
