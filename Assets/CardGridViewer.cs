using Engine;
using SoloTrainGame.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGridViewer : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup;
    [SerializeField]
    private CardUIObject _enlargedCard;

    private List<CardUIObject> _cards;

    private void Awake()
    {
        _cards = new List<CardUIObject>();
        _enlargedCard.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        CloseViewer();
    }

    private void EnlargeCard(CardUIObject cardInstance)
    {
        _enlargedCard.gameObject.SetActive(true);
        _enlargedCard.SetCard(cardInstance.CardInstance);
    }

    public void CloseViewer()
    {
        foreach (CardUIObject card in _cards)
        {
            card.CardClicked.RemoveListener(EnlargeCard);
        }
        _enlargedCard.gameObject.SetActive(false);
    }
  
    public void OpenViewer(List<CardInstance> cards)
    {
        foreach (CardInstance card in cards)
        {
            
            CardUIObject cardUI = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
            RectTransform container = new GameObject("CardContainer").AddComponent<RectTransform>();
            cardUI.RectTransform.SetParent(container);
            cardUI.SetCard(card);
            container.SetParent(_gridLayoutGroup.transform);
            container.localScale = Vector3.one;
            cardUI.CardClicked.AddListener(EnlargeCard);
        }
    }

    
}
