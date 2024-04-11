using UnityEngine;
using UnityEngine.Events;

namespace SoloTrainGame.UI
{
    /// <summary>
    /// Global listenable events
    /// </summary>
    public class CoreGUIEvents
    {
        public UnityEvent<Vector2> WorldDraggedEvent;

        private CoreGUI _gui;

        public CoreGUIEvents(CoreGUI gui)
        {
            _gui = gui;
            WorldDraggedEvent = new UnityEvent<Vector2>();
            _gui.WorldDrag.OnDragEvent.AddListener(WorldDragged);
        }

        ~CoreGUIEvents()
        {
            _gui.WorldDrag.OnDragEvent.RemoveListener(WorldDragged);
        }

        private void WorldDragged(Vector2 delta)
        {
            WorldDraggedEvent?.Invoke(delta);
        }
    }

}