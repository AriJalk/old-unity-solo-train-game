using CommonEngine.ResourceManagement;
using CommonEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using CommonEngine.IO;

namespace CommonEngine.Core
{
	public class CommonServices : MonoBehaviour
	{
		public PrefabManager PrefabManager;

		public readonly InputEvents InputEvents = new InputEvents();
		public readonly CommonEngineEvents CommonEngineEvents = new CommonEngineEvents();
		public readonly RaycastConfig RaycastConfig = new RaycastConfig();
		public readonly MaterialManager MaterialManager = new MaterialManager();

		public readonly InputLock InputLock = new InputLock();
	}
}
