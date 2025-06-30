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
		public event Action<ProtoCardData> CardAddedToHandEvent;
		public event Action<Guid> CardRemovedFromHandEvent;

		public void RaiseCardAddedToHandEvent(ProtoCardData card)
		{
			CardAddedToHandEvent?.Invoke(card);
		}

		public void RaiseCardRemovedFromHandEvent(Guid cardId)
		{
			CardRemovedFromHandEvent?.Invoke(cardId);
		}
	}
}
