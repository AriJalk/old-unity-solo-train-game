namespace SoloTrainGame.UI
{
    public class GUIServices
    {
        private GraphicUserInterface _ui;

        public bool IsUILocked
        {
            get
            {
                return _ui.IsUILocked;
            }
        }

        public GUIEvents GUIEvents { get; private set; }

        public UICardView CardView { get; private set; }
        public UIElementClickable BackgroundImage { get; private set; }

        public GUIServices(GraphicUserInterface ui)
        {
            _ui = ui;
            _ui.Initialize();
            GUIEvents = _ui.GUIEvents;
            CardView = _ui.CardView;
            BackgroundImage = _ui.BackgroundImage;
        }

        public void AddBlocker(UIBlocker blocker)
        {
            if (blocker != null)
            {
                _ui.AddBlocker(blocker);
            }
        }

        public void RemoveBlocker(UIBlocker blocker)
        {
            if (blocker != null)
            {
                _ui.RemoveBlocker(blocker);
            }
        }

        public void SetStateMessage(string message)
        {
            if (_ui.StateMessageText != null)
            {
                _ui.StateMessageText.text = message;
            }
        }

        public void SetExtraMessage(string message)
        {
            if (_ui.ExtraMessageText != null)
            {
                _ui.ExtraMessageText.text = message;
            }
        }
    }
}