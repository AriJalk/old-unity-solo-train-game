using CommonEngine.EngineEvents;
using CommonEngine.IO;
using CommonEngine.ResourceManagement;
using UnityEngine;

namespace CommonEngine.Core
{
	/// <summary>
	/// Main API for CommonEngine
	/// </summary>
	public class CommonServices : MonoBehaviour
	{
		[SerializeField]
		private InputManager _inputManager;

		public PrefabManager PrefabManager;

		public readonly InputEvents InputEvents = new InputEvents();
		public readonly CommonEngineEvents CommonEngineEvents = new CommonEngineEvents();
		public readonly RaycastConfig RaycastConfig = new RaycastConfig();
		public readonly MaterialManager MaterialManager = new MaterialManager();

		public readonly InputLock InputLock = new InputLock();

		public Vector2 CurrentMousePosition
		{
			get
			{
				return _inputManager.CurrentMousePosition;
			}
		}

		public Vector2 CurrentMouseDelta
		{
			get
			{
				return _inputManager.CurrentMouseDelta;
			}
		}
	}
}
