using PrototypeGame.Events.CommandRequestEvents;
using PrototypeGame.Logic.Components.Cards;
using PrototypeGame.Logic.State.Cards;
using System;


namespace PrototypeGame.Events.CommandEventHandlers
{
	internal class CardCommandEventsHandler : IDisposable
	{
		private LogicCardStateManager _logicCardStateManager;
		private CardCommandRequestEvents _cardCommandRequestEvents;

		private SceneCardEvents _sceneCardEvents;

		public CardCommandEventsHandler(LogicCardStateManager logicCardStateManager, CardCommandRequestEvents cardCommandRequestEvents, SceneCardEvents sceneCardEvents) 
		{
			_logicCardStateManager = logicCardStateManager;
			_cardCommandRequestEvents = cardCommandRequestEvents;
			_sceneCardEvents = sceneCardEvents;

			_cardCommandRequestEvents.PlayCardActionRequestEvent += OnPlayCardActionRequest;

			_cardCommandRequestEvents.MoveCardFromHandToDiscardRequestEvent += OnMoveCardFromHandToDiscardRequest;
			_cardCommandRequestEvents.MoveCardFromDiscardToHandRequestEvent += OnMoveCardFromDiscardToHandRequest;
		}

		public void Dispose()
		{
			_cardCommandRequestEvents.PlayCardActionRequestEvent -= OnPlayCardActionRequest;

			_cardCommandRequestEvents.MoveCardFromHandToDiscardRequestEvent -= OnMoveCardFromHandToDiscardRequest;
			_cardCommandRequestEvents.MoveCardFromDiscardToHandRequestEvent -= OnMoveCardFromDiscardToHandRequest;
		}

		private void OnPlayCardActionRequest(Guid cardId)
		{
			_logicCardStateManager.PlayActionFromCard(cardId);
		}

		private void OnMoveCardFromHandToDiscardRequest(Guid cardId)
		{
			ProtoCardData card = _logicCardStateManager.LogicCardState.CardsInHand[cardId];
			_logicCardStateManager.RemoveCardFromHand(card);
			_logicCardStateManager.AddCardToDiscardPile(card);
			_sceneCardEvents.RaiseCardRemovedFromHandEvent(cardId);
		}

		private void OnMoveCardFromDiscardToHandRequest(Guid cardId)
		{
			ProtoCardData card = _logicCardStateManager.LogicCardState.CardsInDiscard[cardId];
			_logicCardStateManager.RemoveCardFromDiscardPile(card);
			_logicCardStateManager.AddCardToHand(card);
			_sceneCardEvents.RaiseCardAddedToHandEvent(card);
			
		}
	}
}
