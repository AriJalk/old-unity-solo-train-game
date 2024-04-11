using Engine;
using SoloTrainGame.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsCubeGameObject : MonoBehaviour
{
    private GoodsCube _cube;

    [SerializeField]
    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(GoodsCube cube)
    {
        _cube = cube;
        Material material = ServiceLocator.MaterialManager.GetWoodColorMaterial(cube.GoodsType.GoodsColor);
    }
}
