using SoloTrainGame.GameLogic;
using UnityEngine.Events;

namespace SoloTrainGame.UI
{
    /// <summary>
    /// Global listenable events
    /// </summary>
    public class LevelEditorGUIEvents : CoreGUIEvents
    {
        private LevelEditorGUI _gui;

        public LevelEditorGUIEvents(LevelEditorGUI gui) : base(gui)
        {
            _gui = gui;
        }

        ~LevelEditorGUIEvents()
        {

        }

    }

}