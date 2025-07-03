using System;
using System.Collections.Generic;

namespace CommonEngine.Core
{
	/// <summary>
	/// Config settings for the camera raycaster
	/// </summary>
	public class RaycastConfig
	{
		private Dictionary<Type, int> _raycastLayers;
		
		public int RaycastLayer { get; private set; }

		public void SetTypeMappings(Dictionary<Type, int> mappings)
		{
			_raycastLayers = mappings;
		}

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
