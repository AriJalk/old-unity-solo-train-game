using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.Logic.State.Cards;
using System;

namespace PrototypeGame.Logic.Services
{
	internal class CardLookupService : ICardLookupService
	{
		private LogicCardState _logicCardState;
		public CardLookupService(LogicCardState logicCardState) 
		{
			_logicCardState = logicCardState;
		}
		public ProtoCardData GetCardData(Guid cardId)
		{
			if (_logicCardState.CardsInHand.ContainsKey(cardId))
			{
				return _logicCardState.CardsInHand[cardId];
			}
			if (_logicCardState.CardsInDiscard.ContainsKey(cardId))
			{
				return _logicCardState.CardsInDiscard[cardId];
			}
			return null;
		}
	}
}
