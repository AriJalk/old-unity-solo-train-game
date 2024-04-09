using SoloTrainGame.GameLogic;
using SoloTrainGame.UI;
using System.Collections.Generic;
using UnityEngine;

public class GraphicUserInterface : MonoBehaviour
{

    public UIHand Hand;
    public UICardView CardView;
    public CardGridViewer CardGridViewer;
    public UIElementClickable BackgroundImage;


    private List<UIBlocker> _blockers;

    public bool IsUILocked
    {
        get
        {
            return _blockers.Count > 0;
        }
    }

    public GUIEvents GUIEvents { get; private set; }

    private CardInstance _selectedCard;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Hand.CardClickedEvent.AddListener(CardClicked);
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
        GUIEvents = new GUIEvents();
    }

    private void OnDestroy()
    {
        Hand.CardClickedEvent.RemoveListener(CardClicked);
    }

    private void CardClicked(CardUIObject card)
    {
        if (card != null)
        {
            GUIEvents.CardClicked?.Invoke(card);
        }
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
