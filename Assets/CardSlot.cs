using Engine;
using SoloTrainGame.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    [SerializeField]
    private Transform _cardTransform;
    
    public CardInstance CardInstance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddCard(CardInstance card)
    {
        if (_cardTransform.childCount == 0)
        {
            CardUIObject cardUI = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
            if(cardUI != null )
            {
                cardUI.RectTransform.SetParent(_cardTransform);
                cardUI.SetCard(card);
                CardInstance = card;
                return true;
            }
        }
        return false;
    }
}
