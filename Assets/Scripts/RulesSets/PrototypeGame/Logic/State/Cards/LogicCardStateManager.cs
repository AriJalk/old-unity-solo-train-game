using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.Logic.ServiceContracts;
using System;
using System.Collections.Generic;

namespace PrototypeGame.Logic.State.Cards
{
	internal class LogicCardStateManager : ICardLookupService
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

		public void MoveCardFromDiscardPileToHand(ProtoCardData card)
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

		public void PlayActionFromCard(Guid cardId)
		{
			ProtoCardData card = LogicCardState.CardsInHand[cardId];
			card.PlayAction();
		}


		#region ICardLookupService
		public ProtoCardData GetCardData(Guid cardId)
		{
			if (LogicCardState.CardsInHand.ContainsKey(cardId))
			{
				return LogicCardState.CardsInHand[cardId];
			}
			if (LogicCardState.CardsInDiscard.ContainsKey(cardId))
			{
				return LogicCardState.CardsInDiscard[cardId];
			}
			return null;
		}

		public IEnumerable<Guid> GetCardsInHand()
		{
			return new List<Guid>(LogicCardState.CardsInHand.Keys);
		}

		public IEnumerable<Guid> GetCardsInDiscardPile()
		{
			return new List<Guid>(LogicCardState.CardsInDiscard.Keys);
		}

		#endregion
	}
}
