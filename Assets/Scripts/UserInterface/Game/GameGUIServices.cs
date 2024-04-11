namespace SoloTrainGame.UI
{
    public class GameGUIServices : CoreGUIServices
    {
        private GameGUI _ui;
        public GameGUIEvents GUIEvents { get; private set; }
        public UICardView CardView { get; private set; }
        public CardSlot CardSlotRed { get; private set; }
        public CardSlot CardSlotBrown { get; private set; }
        public CardSlot CardSlotGray {  get; private set; }
        public UIElementClickable BackgroundImage { get; private set; }
        public UIHand UIHand { get; private set; }

        public GameGUIServices(GameGUI ui) : base(ui)
        {
            _ui = ui;
            _ui.Initialize();
            GUIEvents = _ui.GUIEvents;
            CardView = _ui.CardView;
            BackgroundImage = _ui.BackgroundImage;
            CardSlotRed = _ui.CardSlotRed;
            CardSlotBrown = _ui.CardSlotBrown;
            CardSlotGray = _ui.CardSlotGray;
            UIHand = _ui.Hand;
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