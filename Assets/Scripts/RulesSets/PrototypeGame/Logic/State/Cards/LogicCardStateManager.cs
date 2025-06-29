using PrototypeGame.Logic.Components.Cards;

namespace PrototypeGame.Logic.State.Cards
{
	internal class LogicCardStateManager
	{
		public readonly LogicCardState LogicCardState;

		public LogicCardStateManager(LogicCardState logicCardState)
		{
			LogicCardState = logicCardState;
		}

		public void AddCardToHand(ProtoCardData card)
		{
			LogicCardState.CardsInHand.Add(card.guid, card);
		}

		public void RemoveCardFromHand(ProtoCardData card)
		{
			LogicCardState.CardsInHand.Remove(card.guid);
		}

		public void AddCardToDiscardPile(ProtoCardData card)
		{
			LogicCardState.CardsInDiscard.Add(card.guid, card);
		}

		public void RemoveCardFromDiscardPile(ProtoCardData card)
		{
			LogicCardState.CardsInDiscard.Remove(card.guid);
		}

		public void DiscardCardFromHandToDiscardPile(ProtoCardData card)
		{
			RemoveCardFromHand(card);
			AddCardToDiscardPile(card);
		}

		public void AddCardFromDiscardPileToHand(ProtoCardData card)
		{
			RemoveCardFromDiscardPile(card);
			AddCardToHand(card);
		}

		public void TransferAllCardsFromDiscardPileToHand()
		{
			foreach (ProtoCardData card in LogicCardState.CardsInDiscard.Values)
			{
				AddCardToHand(card);
			}

			LogicCardState.CardsInDiscard.Clear();
		}
	}
}
