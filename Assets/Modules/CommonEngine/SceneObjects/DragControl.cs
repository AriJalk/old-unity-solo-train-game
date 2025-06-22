using CommonEngine.Core;
using CommonEngine.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragControl : MonoBehaviour, IDragHandler
{
	[SerializeField]
	private CommonServiceLocator _serviceLocator;


	private InputEvents _inputEvents;

	private bool _isDragging = false;

	private void Start()
	{
		_inputEvents = _serviceLocator.InputEvents;
		_inputEvents.MouseButtonClickedDownEvent?.AddListener(OnButtonClickedDown);
		_inputEvents.MouseButtonClickedUpEvent?.AddListener(OnButtonClickedUp);
	}

	private void OnDestroy()
	{
		_inputEvents.MouseButtonClickedDownEvent?.RemoveListener(OnButtonClickedDown);
		_inputEvents.MouseButtonClickedUpEvent?.RemoveListener(OnButtonClickedUp);
	}

	private void OnButtonClickedDown(int button, Vector2 position)
	{
		if (button == 1)
		{
			_isDragging = true;
		}
	}

	private void OnButtonClickedUp(int button, Vector2 position)
	{
		if (button == 1)
		{
			_isDragging = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (_isDragging)
		{
			_inputEvents.WorldDraggedEvent?.Invoke(eventData.delta);
		}
	}
}
