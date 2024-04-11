using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SoloTrainGame.UI
{
    public class CoreGUI : MonoBehaviour
    {
        private List<UIBlocker> _blockers;

        public bool IsUILocked
        {
            get
            {
                return _blockers.Count > 0;
            }
        }

        public virtual void Initialize() 
        {
            _blockers = new List<UIBlocker>();
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
