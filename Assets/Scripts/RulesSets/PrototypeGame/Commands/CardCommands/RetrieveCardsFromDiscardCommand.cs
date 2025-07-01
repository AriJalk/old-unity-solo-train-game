using PrototypeGame.Events.CommandRequestEvents;
using System;
using System.Collections.Generic;
using TurnBasedHexEngine.Commands;

namespace Assets.Scripts.RulesSets.PrototypeGame.Commands.CardCommands
{
	internal class RetrieveCardsFromDiscardCommand : ICommand
	{
		private CardCommandRequestEvents _cardCommandRequestEvents;
		private IEnumerable<Guid> _cardsIdCollection;

		public RetrieveCardsFromDiscardCommand(CardCommandRequestEvents cardCommandRequestEvents, IEnumerable<Guid> cardsIdCollection)
		{
			_cardCommandRequestEvents = cardCommandRequestEvents;
			_cardsIdCollection = cardsIdCollection;
		}
		public void Execute()
		{
			foreach (Guid cardId in _cardsIdCollection)
			{
				_cardCommandRequestEvents.RaiseMoveCardFromDiscardToHandRequestEvent(cardId);
			}
		}

		public void Undo()
		{
			foreach (Guid cardId in _cardsIdCollection)
			{
				_cardCommandRequestEvents.RaiseMoveCardFromHandToDiscardRequestEvent(cardId);
			}
		}
	}
}
