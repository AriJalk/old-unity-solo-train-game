using System;

namespace PrototypeGame.Events.CommandRequestEvents
{
	internal class CardCommandRequestEvents
	{
		public event Action<Guid> PlayCardActionRequestEvent;

		public event Action<Guid> MoveCardFromHandToDiscardRequestEvent;
		public event Action<Guid> MoveCardFromDiscardToHandRequestEvent;
		public event Action<Guid> RestoreCardFromDiscardToHandRequestEvent;

		public void RaisePlayCardActionRequestEvent(Guid cardId)
		{
			PlayCardActionRequestEvent?.Invoke(cardId);
		}

		public void RaiseMoveCardFromHandToDiscardRequestEvent(Guid cardId)
		{
			MoveCardFromHandToDiscardRequestEvent?.Invoke(cardId);
		}

		public void RaiseMoveCardFromDiscardToHandRequestEvent(Guid cardId)
		{
			MoveCardFromDiscardToHandRequestEvent?.Invoke(cardId);
		}
		
		public void RaiseRestoreCardFromDiscardToHandRequestEvent(Guid cardId)
		{
			RestoreCardFromDiscardToHandRequestEvent?.Invoke(cardId);
		}
	}
}
