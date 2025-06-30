using System;

namespace PrototypeGame.Events.CommandRequestEvents
{
	internal class CardCommandRequestEvents
	{
		public event Action<Guid> PlayCardActionRequestEvent;

		public void RaisePlayCardActionRequestEvent(Guid cardId)
		{
			PlayCardActionRequestEvent?.Invoke(cardId);
		}
	}
}
