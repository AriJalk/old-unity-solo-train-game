using System;

namespace PrototypeGame.Events.CommandRequestEvents
{
	internal class CardCommandRequestEvents
	{
		public event Action<Guid> PlayCardActionRequestEvent;

		public event Action<Guid, bool> MoveCardFromHandToDiscardRequestEvent;
		public event Action<Guid, bool> MoveCardFromDiscardToHandRequestEvent;

		public event Action ReorganizeCardsInHandRequestEvent;

		public void RaisePlayCardActionRequestEvent(Guid cardId)
		{
			PlayCardActionRequestEvent?.Invoke(cardId);
		}

		public void RaiseMoveCardFromHandToDiscardRequestEvent(Guid cardId, bool fromUndo)
		{
			MoveCardFromHandToDiscardRequestEvent?.Invoke(cardId, fromUndo);
		}

		public void RaiseMoveCardFromDiscardToHandRequestEvent(Guid cardId, bool fromUndo)
		{
			MoveCardFromDiscardToHandRequestEvent?.Invoke(cardId, fromUndo);
		}

		public void RaiseReorganizeHandRequestEvent()
		{
			ReorganizeCardsInHandRequestEvent?.Invoke();
		}
	}
}
