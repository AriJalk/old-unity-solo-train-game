namespace SoloTrainGame.UI
{
    public class CoreGUIServices
    {
        private CoreGUI _ui;

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