using SoloTrainGame.Core;
using System.Collections.Generic;

namespace SoloTrainGame.GameLogic
{
    public class GameState
    {
        private readonly HexGridController _gridController;

        public readonly List<CardInstance> CardHand;
        public readonly Stack<CardInstance> DiscardPile;
        public readonly Stack<CardInstance> BrownDeck;
        public readonly Stack<CardInstance> GreyDeck;
        public readonly Stack<CardInstance> RedDeck;
        public readonly List<CardInstance> CardDisplay;
        public readonly List<CardInstance> RemovedCards;

        public GameState(HexGridController gridController)
        {
            _gridController = gridController;

            CardHand = new List<CardInstance>();
            DiscardPile = new Stack<CardInstance>();
            BrownDeck = new Stack<CardInstance>();
            GreyDeck = new Stack<CardInstance>();
            RedDeck = new Stack<CardInstance>();
            CardDisplay = new List<CardInstance>();
            RemovedCards = new List<CardInstance>();
        }

        public void RefreshDisplay()
        {
            while (CardDisplay.Count > 0)
            {
                CardInstance card = CardDisplay[0];
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