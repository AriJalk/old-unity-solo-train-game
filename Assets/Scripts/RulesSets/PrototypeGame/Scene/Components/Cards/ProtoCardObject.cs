using CardSystem;
using System;
using TMPro;
using Unity.VisualScripting;

namespace PrototypeGame.Scene.Components.Cards
{
	internal class ProtoCardObject : CardObjectBase, IIdentifiable
	{
		public TextMeshProUGUI CardTitle;
		public TextMeshProUGUI MoneyLabel;
		public TextMeshProUGUI TransportPointsLabel;
		public TextMeshProUGUI CardDescription;

		public Guid guid { get; set; }
	}
}
