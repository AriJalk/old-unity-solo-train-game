using UnityEngine;
using UnityEngine.Events;

namespace GameEngine.Core
{
	public class SceneEvents : MonoBehaviour
	{
		public readonly UnityEvent<RaycastHit> ColliderSelectedEvent = new UnityEvent<RaycastHit>();
	}
}