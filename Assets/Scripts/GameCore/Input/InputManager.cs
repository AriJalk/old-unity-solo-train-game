using UnityEngine;
using UnityEngine.Events;

namespace SoloTrainGame.Core
{
    public class InputManager
    {
        public UnityEvent <Vector2> MainMouseButtonClickedDownEvent;
        public UnityEvent<int> MouseButtonHeldEvent;
        public UnityEvent<Vector2> MouseMovedEvent;

        public GraphicUserInterface GUI {  get; set; }
        
        public InputManager(GraphicUserInterface gui)
        {
            MainMouseButtonClickedDownEvent = new UnityEvent<Vector2>();
            MouseButtonHeldEvent = new UnityEvent<int>();
            MouseMovedEvent = new UnityEvent<Vector2>();
            GUI = gui;
        }

        public void Update()
        {
            if (GraphicUserInterface.IsMouseOver == false)
            {
                ProccessMouseButtons();
            }
        }

        private void ProccessMouseButtons()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MainMouseButtonClickedDownEvent.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButtonDown(1))
            {

            }
        }
    }
}