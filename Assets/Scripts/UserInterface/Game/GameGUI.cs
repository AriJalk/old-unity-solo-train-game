using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SoloTrainGame.UI
{
    public class GameGUI : CoreGUI
    {

        public UIHand Hand;
        public UICardView CardView;
        public CardGridViewer CardGridViewer;
        public UIElementClickable BackgroundImage;
        public TextMeshProUGUI StateMessageText;
        public TextMeshProUGUI ExtraMessageText;


        public CardSlot CardSlotRed;
        public CardSlot CardSlotBrown;
        public CardSlot CardSlotGray;

        public GameGUIEvents GameGUIEvents { get; private set; }


        private CardInstance _selectedCard;

        private void Awake()
        {

        }
        // Start is called before the first frame update
        void Start()
        {
            
            CardView.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Initialize()
        {
            base.Initialize();
            if (Screen.width < Screen.height)
            {
                RectUtilities.SetAnchorsAndResetSize(Hand.RectTransform, new Vector2(0.05f, 0f), new Vector2(0.95f, 0.3f));
            }
            Hand.Initialize();
            GameGUIEvents = new GameGUIEvents(this);
        }

        private void OnDestroy()
        {

        }

        private void BackgroundClicked(UIElementClickable element)
        {
            if (CardView.enabled)
            {
                CardView.CloseView();
            }
        }
    }

}
