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
		public event Action<ProtoCardData> CardCreatedAndAddedToHandEvent;

		public void RaiseCardCreatedAndAddedToHandEvent(ProtoCardData card)
		{
			CardCreatedAndAddedToHandEvent?.Invoke(card);
		}
	}
}
