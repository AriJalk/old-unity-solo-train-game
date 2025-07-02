using PrototypeGame.Logic.Components.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeGame.Events
{
	internal class SceneCardEvents
	{
		public event Action<ProtoCardData, bool> CardAddedToHandEvent;
		public event Action<Guid, bool> CardRemovedFromHandEvent;

		public void RaiseCardAddedToHandEvent(ProtoCardData cardData, bool fromUndo)
		{
			CardAddedToHandEvent?.Invoke(cardData, fromUndo);
		}

		public void RaiseCardRemovedFromHandEvent(Guid cardId, bool fromUndo)
		{
			CardRemovedFromHandEvent?.Invoke(cardId, fromUndo);
		}
	}
}
