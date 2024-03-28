using HexSystem;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using UnityEngine;

public class HexTileObject : MonoBehaviour
{
    public Transform CachedTransform { get; private set; }
    public HexData HexData { get; set; }

    public List<HexTileObject> Neighbors { get; private set; }

    public MeshRenderer MeshRenderer;

    private void Awake()
    {
        CachedTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(HexData hexData)
    {
        HexData = hexData;
        Neighbors = new List<HexTileObject>();
    }
}
