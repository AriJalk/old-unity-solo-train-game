namespace SoloTrainGame.UI
{
    public class LevelEditorGUIServices : CoreGUIServices
    {
        private LevelEditorGUI _ui;
        public LevelEditorGUIEvents LevelEditorGUIEvents { get; private set; }

        public LevelEditorGUIServices(LevelEditorGUI ui) : base(ui)
        {
            _ui = ui;
            LevelEditorGUIEvents = _ui.LevelEditorGUIEvents;

        }
    }
}