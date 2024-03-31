using SoloTrainGame.Core;
using System.Collections.Generic;

namespace SoloTrainGame.GameLogic
{
    public class GameState
    {
        private readonly HexGridController _gridController;

        public readonly List<Card> CardHand;
        public readonly Stack<Card> DiscardPile;
        public readonly Stack<Card> BrownDeck;
        public readonly Stack<Card> GreyDeck;
        public readonly Stack<Card> RedDeck;
        public readonly List<Card> CardDisplay;
        public readonly List<Card> RemovedCards;

        public GameState(HexGridController gridController)
        {
            _gridController = gridController;

            CardHand = new List<Card>();
            DiscardPile = new Stack<Card>();
            BrownDeck = new Stack<Card>();
            GreyDeck = new Stack<Card>();
            RedDeck = new Stack<Card>();
            CardDisplay = new List<Card>();
            RemovedCards = new List<Card>();
        }

        public void RefreshDisplay()
        {
            while (CardDisplay.Count > 0)
            {
                Card card = CardDisplay[0];
                RemovedCards.Add(card);
            }
            if (BrownDeck.Count > 0)
            {
                CardDisplay.Add(BrownDeck.Pop());
            }
            if (GreyDeck.Count > 0)
            {
                CardDisplay.Add(GreyDeck.Pop());
            }
            if (RedDeck.Count > 0)
            {
                CardDisplay.Add(RedDeck.Pop());
            }
        }

        public void RetrieveHand()
        {
            while (DiscardPile.Count > 0)
            {
                CardHand.Add(DiscardPile.Pop());
            }
        }
    }
}