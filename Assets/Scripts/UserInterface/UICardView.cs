using Engine;
using Engine.ResourceManagement;
using SoloTrainGame.GameLogic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    private void CloseView()
    {
        ResetView();
        gameObject.SetActive(false);
    }

    private void PlayAction()
    {
        if (_cardUI != null && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)))
        {
            Debug.Log(_cardUI.CardInstance.CardData);
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GraphicUserInterface.IsMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GraphicUserInterface.IsMouseOver = false;
    }
}
