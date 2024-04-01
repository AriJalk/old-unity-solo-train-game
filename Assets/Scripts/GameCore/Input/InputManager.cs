using UnityEngine;
using UnityEngine.Events;

namespace SoloTrainGame.Core
{
    public class InputManager
    {
        public UnityEvent <int, Vector2> MouseButtonClickedDownEvent;
        public UnityEvent<int, Vector2> MouseButtonHeldEvent;
        public UnityEvent<Vector2> MouseMovedEvent;
        public UnityEvent<Vector2> AxisMovedEvent;

        public bool IsButtonHeld { get; private set; }

        public InputManager()
        {
            MouseButtonClickedDownEvent = new UnityEvent<int, Vector2>();
            MouseButtonHeldEvent = new UnityEvent<int, Vector2>();
            MouseMovedEvent = new UnityEvent<Vector2>();
            AxisMovedEvent = new UnityEvent<Vector2>();
        }

        public void UpdateInput()
        {
            if (!GraphicUserInterface.IsMouseOver)
            {
                ProccessMouseButtons();
            }
            ProccessAxis();
        }

        private void ProccessMouseButtons()
        {
            Vector2 position = Input.mousePosition;
            Vector2 movement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            if (Input.GetMouseButtonDown(0))
                MouseButtonClickedDownEvent?.Invoke(0, position);

            else if (Input.GetMouseButtonDown(1))
                MouseButtonClickedDownEvent?.Invoke(1, position);

            else if (Input.GetMouseButton(0))
            {
                MouseButtonHeldEvent?.Invoke(0, movement);
                IsButtonHeld = true;
            }

            else if (Input.GetMouseButton(1))
            {
                MouseButtonHeldEvent?.Invoke(1, movement);
                IsButtonHeld = true;
            }
            // Prevent ui locking movement when held
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                IsButtonHeld = false;
        }

        private void ProccessAxis()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0 || verticalInput != 0)
                AxisMovedEvent?.Invoke(new Vector2(horizontalInput, verticalInput));
        }
    }
}