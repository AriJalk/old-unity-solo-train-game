using CardSystem;
using PrototypeGame.Scene.Components.Cards;
using System;
using System.Collections.Generic;

namespace PrototypeGame.Scene.State.Cards
{
	internal class SceneCardState
	{
		public readonly Dictionary<Guid, ProtoCardObject> Cards;

		public SceneCardState()
		{
			Cards = new Dictionary<Guid, ProtoCardObject>();
		}
	}
}
