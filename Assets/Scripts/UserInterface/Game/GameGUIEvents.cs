using SoloTrainGame.GameLogic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace SoloTrainGame.UI
{
    /// <summary>
    /// Global listenable events
    /// </summary>
    public class GameGUIEvents : CoreGUIEvents
    {
        public UnityEvent<CardUIObject> CardClickedEvent;
        public UnityEvent<Vector2> WorldDraggedEvent;
        public UnityEvent<CardInstance> PlayActionEvent;

        private GameGUI _gui;

        public GameGUIEvents(GameGUI gui) : base(gui)
        {
            _gui = gui;
            CardClickedEvent = new UnityEvent<CardUIObject>();

            WorldDraggedEvent = new UnityEvent<Vector2>();
            _gui.WorldDrag.OnDragEvent.AddListener(WorldDragged);

            _gui.Hand.CardClickedEvent.AddListener(CardClicked);

            _gui.CardView.PlayActionEvent?.AddListener(PlayAction);
        }

        ~GameGUIEvents()
        {
            _gui.Hand.CardClickedEvent.RemoveListener(CardClicked);
            _gui.WorldDrag.OnDragEvent.RemoveListener(WorldDragged);
            _gui.CardView.PlayActionEvent?.RemoveListener(PlayAction);
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

        private void PlayAction(CardInstance card)
        {
            PlayActionEvent?.Invoke(card);
        }

    }

}