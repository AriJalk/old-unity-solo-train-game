using Engine;
using Engine.ResourceManagement;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHand : MonoBehaviour
{
    [SerializeField]
    private HorizontalLayoutGroup content;

    private List<CardUIObject> _cardsHand;
    private Transform _parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        _parentTransform = transform.parent;
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
        Debug.Log(card.CardInstance.CardData.CardType);
        UICardView view = ServiceLocator.PrefabManager.RetrievePoolObject<UICardView>();
        RectUtilities.SetParentAndResetPosition(view.TransformCache, _parentTransform);
        view.SetCard(card.CardInstance);
        
    }

    private void BuildTestHand()
    {

        foreach (CardSO cardSO in ServiceLocator.ScriptableObjectManager.CardTypes)
        {
            CardInstance cardData = new CardInstance(cardSO);
            CardUIObject cardObject = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
            cardObject.SetCard(cardData);
            cardObject.transform.SetParent(content.transform);
            cardObject.CardInstance.CardData.CardBehavior.StartBehavior(cardSO);
            cardObject.CardClicked.AddListener(CardClicked);
            _cardsHand.Add(cardObject);
        }
    }
}
