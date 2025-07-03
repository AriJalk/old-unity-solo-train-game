using CommonEngine.Core;
using CommonEngine.EngineEvents;
using CommonEngine.IO;
using UnityEngine;


namespace CommonEngine.SceneObjects
{
	/// <summary>
	/// Provides listenable raycasting functinoality
	/// </summary>
	public class ClickRaycaster : MonoBehaviour
	{
		[SerializeField]
		private Camera _camera;
		[SerializeField]
		private CommonServices _commonServices;

		private InputEvents _inputEvents;
		private CommonEngineEvents _sceneEvents;
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			_inputEvents = _commonServices.InputEvents;
			_sceneEvents = _commonServices.CommonEngineEvents;
			_inputEvents.MouseButtonClickedUpEvent += OnClick;
		}

		private void OnDestroy()
		{
			_inputEvents.MouseButtonClickedUpEvent -= OnClick;
		}

		// Update is called once per frame
		void Update()
		{

		}

		private void OnClick(int button, Vector2 position)
		{
			if (!_commonServices.InputLock.IsInputLocked && button == 0)
			{
				RaycastHit hit = Raycast(_camera.ScreenPointToRay(position), _commonServices.RaycastConfig.RaycastLayer);
				if (hit.collider != null)
				{
					_sceneEvents.RaiseColliderSelectedEvent(hit);
				}
			}
		}

		private static RaycastHit Raycast(Ray ray, int mask = -1)
		{
			RaycastHit hit;
			Physics.Raycast(ray, out hit, 15f, mask);
			return hit;
		}
	}
}