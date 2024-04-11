namespace SoloTrainGame.UI
{
    public class CoreGUIServices
    {
        private CoreGUI _ui;

        public CoreGUIEvents CoreGUIEvents { get ; private set; }

        public bool IsUILocked
        {
            get
            {
                return _ui.IsUILocked;
            }
        }

     
        public CoreGUIServices(CoreGUI ui)
        {
            _ui = ui;
            _ui.Initialize();
            CoreGUIEvents = _ui.CoreGUIEvents;
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
    }
}