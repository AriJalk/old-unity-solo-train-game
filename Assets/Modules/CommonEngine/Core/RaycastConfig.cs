using PrototypeGame.Scene;
using System;
using System.Collections.Generic;

namespace CommonEngine.Core
{
	public class RaycastConfig
	{
		private Dictionary<Type, int> _raycastLayers = new Dictionary<Type, int>()
		{
			{typeof(HexTileObject), 1 << 6 },
			{typeof(GoodsCubeObject), 1 << 7 },
			{typeof(GoodsCubeSlotObject), 1 << 8 },
		};

		public int RaycastLayer { get; private set; }

		public void SetRaycastLayer<T>()
		{
			if (_raycastLayers.ContainsKey(typeof(T)))
			{
				RaycastLayer = _raycastLayers[typeof(T)];
			}
		}

		public void SetRaycastLayer(params Type[] types) 
		{
			int mask = 0;
			foreach (Type type in types)
			{
				mask = mask | _raycastLayers[type];
			}

			RaycastLayer = mask;
		}
	}
}
