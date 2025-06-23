using CommonEngine.ResourceManagement;
using CommonEngine.Events;
using UnityEngine;

namespace CommonEngine.Core
{
	public class CommonServices : MonoBehaviour
	{
		public PrefabManager PrefabManager;

		public readonly InputEvents InputEvents = new InputEvents();
		public readonly SceneEvents SceneEvents = new SceneEvents();
		public readonly CommonConfig CommonConfig = new CommonConfig();
	}
}
