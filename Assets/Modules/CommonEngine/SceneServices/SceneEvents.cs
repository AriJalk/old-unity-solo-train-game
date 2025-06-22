using UnityEngine;
using UnityEngine.Events;

namespace GameEngine.Core
{
	public class SceneEvents
	{
		public readonly UnityEvent<RaycastHit> ColliderSelectedEvent = new UnityEvent<RaycastHit>();
	}
}