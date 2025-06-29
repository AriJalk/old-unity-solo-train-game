using System;
using Unity.VisualScripting;

namespace PrototypeGame.Logic.Components.Cards
{
	internal class ProtoCardData : IIdentifiable
	{
		public Guid guid { get; private set; }

		public string CardTitle {  get; private set; }
		public int MoneyValue { get; private set; }
		public int TransportPointsValue { get; private set; }
		public string CardDescription { get; private set; }

		public ProtoCardData(Guid guid, string cardTitle, int moneyValue, int transportPointsValue, string cardDescription)
		{
			this.guid = guid;
			CardTitle = cardTitle;
			MoneyValue = moneyValue;
			TransportPointsValue = transportPointsValue;
			CardDescription = cardDescription;
		}
	}
}
