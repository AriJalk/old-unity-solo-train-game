using CardGame.Scene;
using System;
using System.Collections.Generic;

namespace CommonEngine.Core
{
	public class CommonConfig
	{
		public Dictionary<Type, int> RaycastLayers = new Dictionary<Type, int>()
		{
			{typeof(HexTileObject), 1 << 6 },
			{typeof(GoodsCubeSlotObject), 1 << 7 },
		};

		public int RaycastLayer;
	}
}
