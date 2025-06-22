using UnityEngine;
using UnityEngine.InputSystem;

namespace CommonEngine.IO
{
	public class InputManager : MonoBehaviour
	{

		public InputEvents InputEvents;

		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{

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
				InputEvents.MouseButtonClickedDownEvent?.Invoke(0, position);

			else if (Mouse.current.rightButton.wasPressedThisFrame)
				InputEvents.MouseButtonClickedDownEvent?.Invoke(1, position);

			if (Mouse.current.leftButton.wasReleasedThisFrame)
				InputEvents.MouseButtonClickedUpEvent?.Invoke(0, position);

			else if (Mouse.current.rightButton.wasReleasedThisFrame)
				InputEvents.MouseButtonClickedUpEvent?.Invoke(1, position);
		}

		private void ProcessAxis()
		{
			Vector2 input = Vector2.zero;

			if (Keyboard.current.wKey.isPressed) input.y += 1;
			if (Keyboard.current.sKey.isPressed) input.y -= 1;
			if (Keyboard.current.aKey.isPressed) input.x -= 1;
			if (Keyboard.current.dKey.isPressed) input.x += 1;

			if (input != Vector2.zero)
				InputEvents.AxisMovedEvent?.Invoke(input.normalized);
		}

		private void ProcessScroll()
		{
			float scroll = Mouse.current.scroll.ReadValue().y;

			if (scroll != 0)
				InputEvents.MouseScrolledEvent.Invoke(scroll);
		}
	}
}
