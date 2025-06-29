using PrototypeGame.Logic.Components.Cards;
using System;
using System.Collections.Generic;

namespace PrototypeGame.Logic.State.Cards
{
	internal class LogicCardState
	{
		public readonly Dictionary<Guid, ProtoCardData> CardsInHand;
		public readonly Dictionary<Guid, ProtoCardData> CardsInDiscard;

		public LogicCardState()
		{
			CardsInHand = new Dictionary<Guid, ProtoCardData>();
			CardsInDiscard = new Dictionary<Guid, ProtoCardData>();
		}

	}
}
