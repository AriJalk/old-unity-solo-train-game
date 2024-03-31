namespace SoloTrainGame.GameLogic
{
    public class Card
    {
        public CardSO CardData { get; private set; }

        public Card(CardSO cardData)
        {
            CardData = cardData;
        }
    }
}
