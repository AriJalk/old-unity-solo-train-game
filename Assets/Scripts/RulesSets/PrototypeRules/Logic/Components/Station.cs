

using PrototypeGame.Scene;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace PrototypeGame.Logic
{
	internal class Station : IIdentifiable
	{
		public Guid guid { get; private set; }

		public GoodsCubeSlot GoodsCubeSlot1 { get; set; }
		public GoodsCubeSlot GoodsCubeSlot2 { get; set; }

		public bool IsUpgraded { get; set; }


		public Station(Guid guid, bool isUpgraded = false)
		{
			this.guid = guid;
			IsUpgraded = isUpgraded;
		}
	}
}
