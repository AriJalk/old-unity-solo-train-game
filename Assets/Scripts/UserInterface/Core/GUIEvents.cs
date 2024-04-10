using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace SoloTrainGame.UI
{
    /// <summary>
    /// Global listenable events
    /// </summary>
    public class GUIEvents
    {
        public UnityEvent<CardUIObject> CardClickedEvent {  get; private set; }
        public UnityEvent<Vector2> WorldDraggedEvent { get; private set; }

        private GraphicUserInterface _gui;

        public GUIEvents(GraphicUserInterface gui)
        {
            _gui = gui;

            CardClickedEvent = new UnityEvent<CardUIObject>();

            WorldDraggedEvent = new UnityEvent<Vector2>();
            _gui.WorldDrag.OnDragEvent.AddListener(WorldDragged);

            _gui.Hand.CardClickedEvent.AddListener(CardClicked);
        }

        ~GUIEvents()
        {
            _gui.Hand.CardClickedEvent.RemoveListener(CardClicked);
            _gui.WorldDrag.OnDragEvent.RemoveListener(WorldDragged);
        }

        private void CardClicked(CardUIObject card)
        {
            if (card != null)
            {
                CardClickedEvent?.Invoke(card);
            }
        }

        private void WorldDragged(Vector2 delta)
        {
            WorldDraggedEvent?.Invoke(delta);
        }

    }

}