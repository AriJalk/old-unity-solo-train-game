using CommonEngine.IO;
using CommonEngine.ResourceManagement;
using GameEngine.Core;
using UnityEngine;

namespace CommonEngine.Core
{
	public class CommonServiceLocator : MonoBehaviour
	{
		public PrefabManager PrefabManager;

		public readonly InputEvents InputEvents = new InputEvents();
		public readonly SceneEvents SceneEvents = new SceneEvents();
	}
}
