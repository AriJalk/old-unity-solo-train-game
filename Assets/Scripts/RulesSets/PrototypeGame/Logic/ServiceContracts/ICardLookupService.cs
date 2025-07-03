using PrototypeGame.Logic.Components.Cards;
using System;
using System.Collections.Generic;

namespace PrototypeGame.Logic.ServiceContracts
{
	internal interface ICardLookupService
	{
		ProtoCardData GetCardData(Guid cardId);
		IEnumerable<Guid> GetCardsInDiscardPile();
		IEnumerable<Guid> GetCardsInHand();
	}
}
