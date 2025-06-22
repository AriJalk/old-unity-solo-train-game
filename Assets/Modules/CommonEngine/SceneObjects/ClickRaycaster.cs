using CommonEngine.Core;
using CommonEngine.IO;
using GameEngine.Core;
using UnityEngine;

public class ClickRaycaster : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
	[SerializeField]
	private CommonServiceLocator _serviceLocator;

	private InputEvents _inputEvents;
	private SceneEvents _sceneEvents;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		_inputEvents = _serviceLocator.InputEvents;
		_sceneEvents = _serviceLocator.SceneEvents;
		_inputEvents.MouseButtonClickedUpEvent?.AddListener(OnClick);
    }

	private void OnDestroy()
	{
		_inputEvents.MouseButtonClickedUpEvent?.RemoveListener(OnClick);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

    private void OnClick(int button, Vector2 position)
    {
        if (button == 0)
        {
			RaycastHit hit = Raycast(_camera.ScreenPointToRay(position), 1 << 6);
			if (hit.collider != null)
			{
				_sceneEvents.ColliderSelectedEvent?.Invoke(hit);
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
