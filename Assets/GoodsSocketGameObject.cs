using Engine;
using SoloTrainGame.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class GoodsSocketGameObject : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _cubeHolderMeshRenderer;
    [SerializeField]
    private GoodsCubeGameObject _cubeObject;

    private GoodsSocket _goodsSocket;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(GoodsSocket socket)
    {
        if (socket != null)
        {
            _goodsSocket = socket;
            Material material = ServiceLocator.MaterialManager.GetWoodColorMaterial(socket.GoodsType.GoodsColor);
            if (material != null)
            {
                _cubeHolderMeshRenderer.material = material;
            }
            _cubeObject.Initialize(new GoodsCube(ServiceLocator.ScriptableObjectManager.GoodsTypes[Enums.GoodsType.None]));
        }

    }
}
