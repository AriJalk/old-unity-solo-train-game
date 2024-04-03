

using Engine;
using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class UIHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public const float CARD_GAP = 1f;
    public const float CARD_ASPECT_RATIO = 0.7159091f;
    public UnityEvent<CardUIObject> CardClickedEvent;

    [SerializeField]
    [Range(1f, 4f)]
    private float _cardSizeMultiplierX = 1f;
    [SerializeField]
    [Range(1f, 4f)]
    private float _cardSizeMultiplierY = 1f;
    [SerializeField]
    private ResizableContent _cardsTransform;

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
        for (int i = 0; i < 10; i++)
        {
            foreach (CardSO cardSO in ServiceLocator.ScriptableObjectManager.CardTypes)
            {
                PlaceCard(cardSO);
            }
        }
    }

    private Vector2 CalculatePosition(int index, Vector2 size)
    {
        float modifier = size.x / 2f;
        float xOffset = size.x * CARD_GAP * 0.5f;
        float x = ((index % 2 == 0) ? 1 : -1) * (xOffset + index / 2 * size.x * CARD_GAP);
        return new Vector2(x, 0);
    }




    private void PlaceCard(CardSO cardSO)
    {
        GameObject container = new GameObject();
        RectTransform containerRectTransform = container.AddComponent<RectTransform>();
        CardInstance cardData = new CardInstance(cardSO);
        CardUIObject cardObject = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
        cardObject.SetCard(cardData);
        cardObject.transform.SetParent(container.transform);
        _cardsTransform.AddToTransform(container.transform);
        containerRectTransform.localScale = Vector2.one;
        float height = _cardsTransform.RectTransform.rect.height -10f;
        Vector2 size = new Vector2(height * CARD_ASPECT_RATIO, height);
        containerRectTransform.sizeDelta = size;
        container.transform.localPosition = CalculatePosition(_cardsHand.Count, containerRectTransform.sizeDelta);
        _cardsTransform.Resize(cardObject.transform);
        cardObject.CardInstance.CardData.CardBehavior.StartBehavior(cardSO);
        cardObject.CardClicked.AddListener(CardClicked);
        _cardsHand.Add(cardObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("HAND ENTER");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("HAND EXIT");
    }
}
