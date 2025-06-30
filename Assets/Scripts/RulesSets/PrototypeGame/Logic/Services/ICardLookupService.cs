using PrototypeGame.Logic.Components.Cards;
using System;

namespace PrototypeGame.Logic.Services
{
	internal interface ICardLookupService
	{
		ProtoCardData GetCardData(Guid cardId);
	}
}
