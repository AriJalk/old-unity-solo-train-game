using SoloTrainGame.GameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUIObject : MonoBehaviour, IPointerCombined
{
    public UnityEvent<CardUIObject> CardClicked;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI TransportText;
    public TextMeshProUGUI DescriptionText;
    public Image Border;

    public CardInstance CardInstance { get; private set; }

    public void Awake()
    {
        CardClicked = new UnityEvent<CardUIObject>();
    }

    public void SetCard(CardInstance card)
    {
        CardInstance = card;

        NameText.text = CardInstance.CardData.Name;
        MoneyText.text = CardInstance.CardData.GeneratedMoney.ToString() + "$";
        TransportText.text = CardInstance.CardData.GeneratedTransport.ToString() + "T";
        DescriptionText.text = CardInstance.CardData.Description;
        switch (card.CardData.CardType)
        {
            case Enums.CardType.Build:
                Border.color = new Color(180f / 255f, 110f / 255f, 45f / 255f);
                break;
            case Enums.CardType.Transport:
                Border.color = Color.gray;
                break;
            case Enums.CardType.Special:
                Border.color = new Color(255f / 255f, 90f / 255f, 90f / 255f);
                break;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("UP CARD, CLICKED");
        CardClicked.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter Card");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Card");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down Card");
    }
}
