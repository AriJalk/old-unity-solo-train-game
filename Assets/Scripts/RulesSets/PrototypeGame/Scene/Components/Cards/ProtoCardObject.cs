using CardSystem;
using System;
using TMPro;
using Unity.VisualScripting;

namespace PrototypeGame.Scene.Components.Cards
{
	internal class ProtoCardObject : CardObjectBase
	{
		public TextMeshProUGUI CardTitle;
		public TextMeshProUGUI MoneyLabel;
		public TextMeshProUGUI TransportPointsLabel;
		public TextMeshProUGUI CardDescription;
	}
}
