using SoloTrainGame.GameLogic;
using SoloTrainGame.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIObject : UIElementClickable
{
    public const int CARD_SIZE_X = 63;
    public const int CARD_SIZE_Y = 88;

    public RectTransform RectTransform;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI TransportText;
    public TextMeshProUGUI DescriptionText;
    public Image Border;
    public CardInstance CardInstance { get; private set; }
    

    public void Awake()
    {

    }


    public void SetCard(CardInstance card)
    {
        CardInstance = card;

        NameText.text = CardInstance.CardData.Name;
        MoneyText.text = CardInstance.CardData.GeneratedMoney.ToString() + "$";
        TransportText.text = CardInstance.CardData.GeneratedTransport.ToString() + "T";
        DescriptionText.text = CardInstance.CardData.Description;
        // TODO: SO with color reference
        switch (card.CardData.CardType)
        {
            case Enums.CardType.Build:
                Border.color = new Color(255f / 255f, 127f / 255f, 80f / 255f);
                break;
            case Enums.CardType.Transport:
                Border.color = new Color(112f / 255f, 128f / 255f, 144f / 255f);
                break;
            case Enums.CardType.Special:
                Border.color = new Color(64f / 255f, 224f / 255f, 208f / 255f);
                break;
        }

    }
}
