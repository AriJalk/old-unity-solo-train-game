using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SoloTrainGame.UI
{
    public class UserInterfaceService
    {
        private GraphicUserInterface _ui;
        
        public bool IsUILocked
        {
            get
            {
                return _ui.IsUILocked;
            }
        }

        public UserInterfaceService(GraphicUserInterface ui)
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