using System;
using UnityEngine;

namespace CommonEngine.EngineEvents
{
	public class CommonEngineEvents
	{
		public event Action<RaycastHit> ColliderSelectedEvent;

		public void RaiseColliderSelectedEvent(RaycastHit hit)
		{
			ColliderSelectedEvent?.Invoke(hit);
		}
	}
}