using UnityEngine;
using UnityEngine.Events;

namespace Engine
{
    public class InputEvents
    {
        public UnityEvent<int, Vector2> MouseButtonClickedDownEvent {  get; private set; }
        public UnityEvent<int, Vector2> MouseButtonClickedUpEvent { get; private set; }
        public UnityEvent<int, Vector2> MouseButtonHeldEvent { get; private set; }
        public UnityEvent<Vector2> MouseMovedEvent { get; private set; }
        public UnityEvent<Vector2> AxisMovedEvent { get; private set; }
        public UnityEvent<float> MouseScrolledEvent { get; private set; }
        

        public InputEvents()
        {
            MouseButtonClickedDownEvent = new UnityEvent<int, Vector2>();
            MouseButtonHeldEvent = new UnityEvent<int, Vector2>();
            MouseMovedEvent = new UnityEvent<Vector2>();
            AxisMovedEvent = new UnityEvent<Vector2>();
            MouseScrolledEvent = new UnityEvent<float>();
            MouseButtonClickedUpEvent = new UnityEvent<int, Vector2>();
        }
    }
}