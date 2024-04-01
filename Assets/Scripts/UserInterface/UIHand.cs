

using Engine;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UIHand : MonoBehaviour
{
    public UnityEvent<CardUIObject> CardClickedEvent;

    [SerializeField]
    private HorizontalLayoutGroup _content;

    private List<CardUIObject> _cardsHand;

    private void Awake()
    {
        CardClickedEvent = new UnityEvent<CardUIObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.PrefabManager.LoadAndRegisterPrefab<CardUIObject>(Engine.ResourceManagement.PrefabFolder.PREFAB_2D, "CardPrefab", 50);
        ServiceLocator.PrefabManager.LoadAndRegisterPrefab<UICardView>(Engine.ResourceManagement.PrefabFolder.PREFAB_2D, "CardViewPrefab", 1);
        _cardsHand = new List<CardUIObject>();
        BuildTestHand();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        foreach (CardUIObject card in _cardsHand)
        {
            card.CardClicked.RemoveListener(CardClicked);
        }
    }


    private void CardClicked(CardUIObject card)
    {
        CardClickedEvent.Invoke(card);
    }

    private void BuildTestHand()
    {

        foreach (CardSO cardSO in ServiceLocator.ScriptableObjectManager.CardTypes)
        {
            CardInstance cardData = new CardInstance(cardSO);
            CardUIObject cardObject = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
            cardObject.SetCard(cardData);
            cardObject.transform.SetParent(_content.transform);
            cardObject.CardInstance.CardData.CardBehavior.StartBehavior(cardSO);
            cardObject.CardClicked.AddListener(CardClicked);
            _cardsHand.Add(cardObject);
        }
    }
}
