using CommonEngine.Core;
using CommonEngine.EngineEvents;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CommonEngine.SceneObjects
{
	/// <summary>
	/// An invisible full screen drag element, used mainly as control method for camera rotation
	/// </summary>
	public class DragControl : MonoBehaviour, IDragHandler
	{
		[SerializeField]
		private CommonServices _commonServices;


		private InputEvents _inputEvents;

		private bool _isDragging = false;

		private void Start()
		{
			_inputEvents = _commonServices.InputEvents;
			_inputEvents.MouseButtonClickedDownEvent += OnButtonClickedDown;
			_inputEvents.MouseButtonClickedUpEvent += OnButtonClickedUp;
		}

		private void OnDestroy()
		{
			_inputEvents.MouseButtonClickedDownEvent -= OnButtonClickedDown;
			_inputEvents.MouseButtonClickedUpEvent -= OnButtonClickedUp;
		}

		private void OnButtonClickedDown(int button, Vector2 position)
		{
			if (!_commonServices.InputLock.IsInputLocked && button == 1)
			{
				_isDragging = true;
			}
		}

		private void OnButtonClickedUp(int button, Vector2 position)
		{
			if (!_commonServices.InputLock.IsInputLocked && button == 1)
			{
				_isDragging = false;
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (_isDragging)
			{
				_inputEvents.RaiseWorldDraggedEvent(eventData.delta);
			}
		}
	}
}