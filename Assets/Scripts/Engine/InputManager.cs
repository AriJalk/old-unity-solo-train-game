using UnityEngine;
using UnityEngine.Events;

namespace Engine
{
    //TODO: 1 mouse button 
    public class InputManager
    {
        public InputEvents InputEvents;

        public bool IsButtonHeld { get; private set; }

        public InputManager()
        {
            InputEvents = new InputEvents();
        }

        public void UpdateInput()
        {
            ProccessMouseButtons();
            ProccessAxis();
            ProccessScroll();
        }

        private void ProccessMouseButtons()
        {
            Vector2 position = Input.mousePosition;
            Vector2 movement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (Input.GetMouseButtonDown(0))
                InputEvents.MouseButtonClickedDownEvent?.Invoke(0, position);

            else if (Input.GetMouseButtonDown(1))
                InputEvents.MouseButtonClickedDownEvent?.Invoke(1, position);

            if (Input.GetMouseButton(0))
            {
                InputEvents.MouseButtonHeldEvent?.Invoke(0, movement);
                IsButtonHeld = true;
            }
            else if (Input.GetMouseButton(1))
            {
                InputEvents.MouseButtonHeldEvent?.Invoke(1, movement);
                IsButtonHeld = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                InputEvents.MouseButtonUpEvent?.Invoke(0, movement);
            }
            if (Input.GetMouseButtonUp(1))
            {
                InputEvents.MouseButtonUpEvent?.Invoke(1, movement);
            }
        }

        private void ProccessAxis()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0 || verticalInput != 0)
                InputEvents.AxisMovedEvent?.Invoke(new Vector2(horizontalInput, verticalInput));
        }

        private void ProccessScroll()
        {
            float scroll = Input.mouseScrollDelta.y;
            if (scroll != 0)
                InputEvents.MouseScrolledEvent.Invoke(scroll);
        }
    }
}