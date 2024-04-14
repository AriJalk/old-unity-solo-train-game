using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SoloTrainGame.UI
{
    public class CoreGUI : MonoBehaviour
    {
        private List<UIBlocker> _blockers;

        public WorldDrag WorldDrag;

        public CoreGUIEvents CoreGUIEvents {  get; private set; }

        public bool IsUIBlocked
        {
            get
            {
                return _blockers.Count > 0;
            }
        }

        private bool _isUILocked;
        public bool IsUILocked
        {
            get
            {
                return _isUILocked || IsUIBlocked;
            }
            set
            {
                _isUILocked = value;
            }
        }

        public virtual void Initialize() 
        {
            _blockers = new List<UIBlocker>();
            WorldDrag.Initialize();
            CoreGUIEvents = new CoreGUIEvents(this);
        }

        private void OnDestroy()
        {

        }

        public void AddBlocker(UIBlocker blocker)
        {
            if (!_blockers.Contains(blocker))
            {
                _blockers.Add(blocker);
            }
        }

        public void RemoveBlocker(UIBlocker blocker)
        {
            if (_blockers.Contains(blocker))
            {
                _blockers.Remove(blocker);
            }
        }
    }

}
