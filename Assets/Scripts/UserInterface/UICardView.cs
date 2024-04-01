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

    private CardInstance _card;
    private CardUIObject _cardUI;

    public Transform TransformCache { get; private set; }

    void Awake()
    {
        TransformCache = transform;
    }

    public void SetCard(CardInstance card)
    {
        _card = card;
        _cardUI = ServiceLocator.PrefabManager.RetrievePoolObject<CardUIObject>();
        _cardUI.SetCard(card);
        RectUtilities.SetParentAndResetPosition(_cardUI.transform, _cardSlotTransform);
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
