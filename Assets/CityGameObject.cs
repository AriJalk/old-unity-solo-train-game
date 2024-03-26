using Engine;
using SoloTrainGame.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGameObject : MonoBehaviour
{
    public MeshRenderer ProductionSlotMesh;
    public MeshRenderer DeliverySlotMesh;

    public ProductionSlot ProductionSlot { get; private set; }
    public DeliverySlot DeliverySlot { get; private set; }

    private void SetProductionSlotMaterial()
    {
        if (ProductionSlot != null)
        {
            
        }
    }
}
