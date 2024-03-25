using HexSystem;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using UnityEngine;

public class HexTileObject : MonoBehaviour
{
    public MapHexData HexData { get; set; }

    public List<HexTileObject> Neighbors { get; private set; }

    public MeshRenderer MeshRenderer;

    
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(MapHexData hexData)
    {
        HexData = hexData;
        Neighbors = new List<HexTileObject>();
    }
}
