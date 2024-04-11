using SoloTrainGame.GameLogic;
using UnityEngine.Events;

namespace SoloTrainGame.UI
{
    /// <summary>
    /// Global listenable events
    /// </summary>
    public class GameGUIEvents : CoreGUIEvents
    {
        public UnityEvent<CardUIObject> CardClickedEvent;
        
        public UnityEvent<CardInstance> PlayActionEvent;

        private GameGUI _gui;

        public GameGUIEvents(GameGUI gui) : base(gui)
        {
            _gui = gui;
            CardClickedEvent = new UnityEvent<CardUIObject>();

           

            _gui.Hand.CardClickedEvent.AddListener(CardClicked);

            _gui.CardView.PlayActionEvent?.AddListener(PlayAction);
        }

        ~GameGUIEvents()
        {
            _gui.Hand.CardClickedEvent.RemoveListener(CardClicked);

            _gui.CardView.PlayActionEvent?.RemoveListener(PlayAction);
        }

        private void CardClicked(CardUIObject card)
        {
            if (card != null)
            {
                CardClickedEvent?.Invoke(card);
            }
        }


        private void PlayAction(CardInstance card)
        {
            PlayActionEvent?.Invoke(card);
        }

    }

}