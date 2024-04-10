using SoloTrainGame.GameLogic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SoloTrainGame.UI
{
    public class GraphicUserInterface : MonoBehaviour
    {

        public UIHand Hand;
        public UICardView CardView;
        public CardGridViewer CardGridViewer;
        public UIElementClickable BackgroundImage;
        public TextMeshProUGUI StateMessageText;
        public TextMeshProUGUI ExtraMessageText;
        public WorldDrag WorldDrag;

        private List<UIBlocker> _blockers;

        public GUIEvents GUIEvents { get; private set; }


        public bool IsUILocked
        {
            get
            {
                return _blockers.Count > 0;
            }
        }

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

        public void Initialize()
        {
            if (Screen.width < Screen.height)
            {
                RectUtilities.SetAnchorsAndResetSize(Hand.RectTransform, new Vector2(0.05f, 0f), new Vector2(0.95f, 0.3f));
            }
            Hand.Initialize();
            _blockers = new List<UIBlocker>();
            WorldDrag.Initialize();
            GUIEvents = new GUIEvents(this);
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
