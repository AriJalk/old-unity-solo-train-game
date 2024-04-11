using Engine;
using SoloTrainGame.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGameObject : MonoBehaviour
{
    [SerializeField]
    private GoodsSocketGameObject _productionSocket;
    [SerializeField]
    private GoodsSocketGameObject _deliverySocket;

    private City _city;

    public void Initialize(City city)
    {
        if(city != null)
        {
            _productionSocket.Initialize(city.ProductionSocket);
            _deliverySocket.Initialize(city.DeliverySocket);
        }
    }
}
