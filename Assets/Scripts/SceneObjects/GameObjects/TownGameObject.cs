using SoloTrainGame.GameLogic;
using UnityEngine;

public class TownGameObject : MonoBehaviour
{
    [SerializeField]
    private GoodsSocketGameObject _productionSocket;

    private Town _town;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Town town)
    {
        _town = town;
        _productionSocket.Initialize(town.ProductionSocket);
    }
}
