using SoloTrainGame.GameLogic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace SoloTrainGame.UI
{
    /// <summary>
    /// Global listenable events
    /// </summary>
    public class CoreGUIEvents
    {
        private CoreGUI _gui;

        public CoreGUIEvents(CoreGUI gui)
        {
            _gui = gui;
        }

        ~CoreGUIEvents()
        {

        }
    }

}