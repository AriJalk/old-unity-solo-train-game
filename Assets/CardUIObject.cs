using SoloTrainGame.GameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIObject : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI TransportText;
    public Image Border;
    public CardInstance Card {  get; private set; }


    public void SetCard(CardInstance card)
    {
        Card = card;

        NameText.text = Card.CardData.Name;
        MoneyText.text = Card.CardData.GeneratedMoney.ToString() + "$";
        TransportText.text = Card.CardData.GeneratedTransport.ToString() + "T";
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
}
