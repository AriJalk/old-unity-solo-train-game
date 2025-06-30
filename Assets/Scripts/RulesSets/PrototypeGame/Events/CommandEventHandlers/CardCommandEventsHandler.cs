using PrototypeGame.Events.CommandRequestEvents;
using PrototypeGame.Logic.State.Cards;
using System;


namespace PrototypeGame.Events.CommandEventHandlers
{
	internal class CardCommandEventsHandler : IDisposable
	{
		private LogicCardStateManager _logicCardStateManager;
		private CardCommandRequestEvents _cardCommandRequestEvents;

		public CardCommandEventsHandler(LogicCardStateManager logicCardStateManager, CardCommandRequestEvents cardCommandRequestEvents) 
		{
			_logicCardStateManager = logicCardStateManager;
			_cardCommandRequestEvents = cardCommandRequestEvents;

			_cardCommandRequestEvents.PlayCardActionRequestEvent += OnPlayCardActionRequest;
		}

		public void Dispose()
		{
			_cardCommandRequestEvents.PlayCardActionRequestEvent -= OnPlayCardActionRequest;
		}

		private void OnPlayCardActionRequest(Guid cardId)
		{
			_logicCardStateManager.PlayActionFromCard(cardId);
		}


	}
}
