using Engine;
using SoloTrainGame.GameLogic;
using SoloTrainGame.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICardView : UIBlocker
{
    public UnityEvent<CardInstance> PlayActionEvent;

    [SerializeField]
    private Transform _cardSlotTransform;
    [SerializeField]
    private Button _playActionButton;
    [SerializeField]
    private Button _backButton;

    private CardUIObject _cardUI;

    public Transform TransformCache { get; private set; }

    void Awake()
    {
        TransformCache = transform;
    }

    private void OnDestroy()
    {
        ResetView();
    }

    private void ResetView()
    {
        if (_cardUI != null)
        {
            ServiceLocator.PrefabManager.ReturnPoolObject(_cardUI);
            _cardUI = null;
            _backButton.onClick.RemoveListener(CloseView);
            _playActionButton.onClick.RemoveListener(PlayAction);
        }
    }

    public void CloseView()
    {
        ResetView();
        gameObject.SetActive(false);
        ServiceLocator.GUIService?.RemoveBlocker(this);
        CanBlock = false;
    }

    private void PlayAction()
    {
        if (_cardUI != null && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)))
        {
            PlayActionEvent?.Invoke(_cardUI.CardInstance);
            CloseView();
        }
    }

    public void SetCard(CardInstance card)
    {
        ResetView();
        _cardUI = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
        _cardUI.SetCard(card);
        RectUtilities.SetParentAndResetPosition(_cardUI.transform, _cardSlotTransform);
        _backButton.onClick.AddListener(CloseView);
        _playActionButton.onClick.AddListener(PlayAction);
        CanBlock = true;
    }
}
