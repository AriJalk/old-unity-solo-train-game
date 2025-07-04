using CommonEngine.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CommonEngine.IO
{
	/// <summary>
	/// Listens to input, and dispatches events accordingly
	/// </summary>
	internal class InputManager : MonoBehaviour
	{
		[SerializeField]
		private CommonServices _commonServices;

		private InputEvents _inputEvents;

		public Vector2 CurrentMousePosition { get; private set; }
		public Vector2 CurrentMouseDelta {  get; private set; }

		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			_inputEvents = _commonServices.InputEvents;
		}

		// Update is called once per frame
		void Update()
		{
			if (_commonServices.InputLock.WasInputReleasedThisFrame)
			{
				return;
			}
			ProcessMouseActions();
			ProcessAxis();
			ProcessScroll();
		}

		void ProcessMouseActions()
		{
			CurrentMousePosition = Mouse.current.position.ReadValue();
			CurrentMouseDelta = Mouse.current.delta.ReadValue();
			//Vector2 movement = Mouse.current.delta.ReadValue();

			if (Mouse.current.leftButton.wasPressedThisFrame)
				_inputEvents.RaiseMouseButtonClickedDown(0, CurrentMousePosition);

			else if (Mouse.current.rightButton.wasPressedThisFrame)
				_inputEvents.RaiseMouseButtonClickedDown(1, CurrentMousePosition);

			if (Mouse.current.leftButton.wasReleasedThisFrame)
				_inputEvents.RaiseMouseButtonClickedUp(0, CurrentMousePosition);

			else if (Mouse.current.rightButton.wasReleasedThisFrame)
				_inputEvents.RaiseMouseButtonClickedUp(1, CurrentMousePosition);
		}

		private void ProcessAxis()
		{
			Vector2 input = Vector2.zero;

			if (Keyboard.current.wKey.isPressed) input.y += 1;
			if (Keyboard.current.sKey.isPressed) input.y -= 1;
			if (Keyboard.current.aKey.isPressed) input.x -= 1;
			if (Keyboard.current.dKey.isPressed) input.x += 1;

			if (input != Vector2.zero)
				_inputEvents.RaiseAxisMovedEvent(input.normalized);
		}

		private void ProcessScroll()
		{
			float scroll = Mouse.current.scroll.ReadValue().y;

			if (scroll != 0)
				_inputEvents.RaiseMouseScrolledEvent(scroll);
		}
	}
}
