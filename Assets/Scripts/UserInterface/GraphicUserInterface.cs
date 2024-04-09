using SoloTrainGame.GameLogic;
using SoloTrainGame.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private CardInstance _selectedCard;

    private void Awake()
    {
        _blockers = new List<UIBlocker>();
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
    }

    private void OnDestroy()
    {
        Hand.CardClickedEvent.RemoveListener(CardClicked);
    }

    private void CardClicked(CardUIObject card)
    {
        if (card != null)
        {
            // TODO: States here
            _selectedCard = card.CardInstance;
            CardView.SetCard(card.CardInstance);
            CardView.gameObject.SetActive(true);
            BackgroundImage.ElementClickedEvent.AddListener(BackgroundClicked);
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
