using UnityEngine;
using UnityEngine.Events;

namespace CommonEngine.SceneServices
{
	public class SceneEvents
	{
		public readonly UnityEvent<RaycastHit> ColliderSelectedEvent = new UnityEvent<RaycastHit>();
	}
}