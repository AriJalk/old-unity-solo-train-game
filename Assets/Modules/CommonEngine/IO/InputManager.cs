using CommonEngine.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CommonEngine.IO
{
	public class InputManager : MonoBehaviour
	{
		[SerializeField]
		private CommonServices _commonServices;

		private InputEvents _inputEvents;

		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			_inputEvents = _commonServices.InputEvents;
		}

		// Update is called once per frame
		void Update()
		{
			ProcessMouseButtons();
			ProcessAxis();
			ProcessScroll();
		}

		void ProcessMouseButtons()
		{
			Vector2 position = Mouse.current.position.ReadValue();
			Vector2 movement = Mouse.current.delta.ReadValue();

			if (Mouse.current.leftButton.wasPressedThisFrame)
				_inputEvents.MouseButtonClickedDownEvent?.Invoke(0, position);

			else if (Mouse.current.rightButton.wasPressedThisFrame)
				_inputEvents.MouseButtonClickedDownEvent?.Invoke(1, position);

			if (Mouse.current.leftButton.wasReleasedThisFrame)
				_inputEvents.MouseButtonClickedUpEvent?.Invoke(0, position);

			else if (Mouse.current.rightButton.wasReleasedThisFrame)
				_inputEvents.MouseButtonClickedUpEvent?.Invoke(1, position);
		}

		private void ProcessAxis()
		{
			Vector2 input = Vector2.zero;

			if (Keyboard.current.wKey.isPressed) input.y += 1;
			if (Keyboard.current.sKey.isPressed) input.y -= 1;
			if (Keyboard.current.aKey.isPressed) input.x -= 1;
			if (Keyboard.current.dKey.isPressed) input.x += 1;

			if (input != Vector2.zero)
				_inputEvents.AxisMovedEvent?.Invoke(input.normalized);
		}

		private void ProcessScroll()
		{
			float scroll = Mouse.current.scroll.ReadValue().y;

			if (scroll != 0)
				_inputEvents.MouseScrolledEvent.Invoke(scroll);
		}
	}
}
