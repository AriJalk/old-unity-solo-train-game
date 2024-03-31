namespace SoloTrainGame.GameLogic
{
    public class CardInstance
    {
        public CardSO CardData { get; private set; }

        public CardInstance(CardSO cardData)
        {
            CardData = cardData;
        }
    }
}
