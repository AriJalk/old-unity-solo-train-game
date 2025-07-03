using PrototypeGame.Logic.Components.Cards;
using System;

namespace PrototypeGame.Events
{
	internal class SceneCardEvents
	{
		public event Action<ProtoCardData, bool> CardAddedToHandEvent;
		public event Action<Guid, bool> CardRemovedFromHandEvent;

		public event Action CardsInHandReorganizedEvent;

		public void RaiseCardAddedToHandEvent(ProtoCardData cardData, bool fromUndo)
		{
			CardAddedToHandEvent?.Invoke(cardData, fromUndo);
		}

		public void RaiseCardRemovedFromHandEvent(Guid cardId, bool fromUndo)
		{
			CardRemovedFromHandEvent?.Invoke(cardId, fromUndo);
		}

		public void RaiseCardsInHandReorganizedEvent()
		{
			CardsInHandReorganizedEvent?.Invoke();
		}
	}
}
