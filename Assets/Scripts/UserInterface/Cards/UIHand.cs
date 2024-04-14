using Engine;
using SoloTrainGame.GameLogic;
using SoloTrainGame.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UIHand : UIBlocker
{
    public const float CARD_GAP = 1f;
    public const float CARD_ASPECT_RATIO = 0.7159091f;
    public const float CARD_PADDING = 10f;
    public UnityEvent<CardUIObject> CardClickedEvent;
    public RectTransform RectTransform;

    [SerializeField]
    private RectTransform _cardsTransform;

    public List<CardUIObject> CardsHand { get; private set; }

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //BuildTestHand();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        foreach (CardUIObject card in CardsHand)
        {
            card.ElementClickedEvent.RemoveListener(CardClicked);
        }
    }

    public void Initialize()
    {
        CardClickedEvent = new UnityEvent<CardUIObject>();
        CardsHand = new List<CardUIObject>();
        ServiceLocator.PrefabManager.LoadAndRegisterPrefab<CardUIObject>(Engine.ResourceManagement.PrefabFolder.PREFAB_2D, "CardPrefab", 50);
        ServiceLocator.PrefabManager.LoadAndRegisterPrefab<UICardView>(Engine.ResourceManagement.PrefabFolder.PREFAB_2D, "CardViewPrefab", 1);
    }


    private void CardClicked(UIElementClickable element)
    {
        CardUIObject card = element.GetComponent<CardUIObject>();
        if (card != null)
        {
            CardClickedEvent.Invoke(card);
        }
    }


    private Vector2 CalculatePosition(int index, Vector2 size)
    {
        float x = index * (size.x + CARD_GAP);
        return new Vector2 (x, 0);
    }

    private Vector2 CalculateEllipsePoint(float angle, float widthRadius, float heightRadius, float widthOffset = 0, float heightOffset = 0)
    {
        float radians = Mathf.Deg2Rad * angle;
        float x = -widthRadius * Mathf.Cos(radians) + widthOffset;
        float y = heightRadius * Mathf.Sin(radians) + heightOffset;
        return new Vector2(x, y);
    }

    public void AddCardToHandFromInstance(CardInstance card)
    {
        // Create container for the card
        GameObject container = new GameObject();
        RectTransform containerRectTransform = container.AddComponent<RectTransform>();
        // Set prefab card to card instance
        CardUIObject cardObject = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
        cardObject.SetCard(card);

        cardObject.RectTransform.SetParent(containerRectTransform);
        containerRectTransform.SetParent(_cardsTransform);
        containerRectTransform.localScale = Vector2.one;

        // Calculate size according to height and aspect ratio

        float height = _cardsTransform.rect.height;
        Vector2 size = new Vector2(height * CARD_ASPECT_RATIO, height);
        containerRectTransform.sizeDelta = size;
        containerRectTransform.localPosition = CalculateEllipsePoint(CardsHand.Count * 20f, RectTransform.rect.width / 2f, RectTransform.rect.height / 2f);

        // Add listener to card click
        //cardObject.CardInstance.CardData.CardBehavior.StartBehavior(cardObject.CardInstance.CardData);
        cardObject.ElementClickedEvent.AddListener(CardClicked);
        CardsHand.Add(cardObject);

        // Resize the hand size so it can be dragged
        //_cardsTransform.Resize(size.x, CardsHand.Count, CARD_GAP);
    }

    public void RemoveCardFromHand(CardInstance card)
    {
        for(int i = 0; i < CardsHand.Count; i++)
        {
            if (CardsHand[i].CardInstance == card)
            {
                CardUIObject cardUI = CardsHand[i];
                Transform container = cardUI.RectTransform.parent; 
                cardUI.ElementClickedEvent.RemoveListener(CardClicked);
                ServiceLocator.PrefabManager.ReturnPoolObject(cardUI);
                CardsHand.RemoveAt(i);
                Destroy(container.gameObject);
                return;
            }
        }

    }
}
