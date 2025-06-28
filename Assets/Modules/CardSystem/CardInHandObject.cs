using CommonEngine.Core;
using CommonEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CardInHandObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private CardServices _cardServices;
	[SerializeField]
	private CommonServices _commonServices;

	private bool _isDragging;

	private RectTransform _rectTransform;

	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (_isDragging)
		{
			_rectTransform.position = Mouse.current.position.ReadValue();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_commonServices.InputLock.AddLock(gameObject);
		Debug.Log("OnPointerDown");
		_isDragging = true;
		_cardServices.BeginCardDrag(this);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_commonServices.InputLock.RemoveLock(gameObject);
		Debug.Log("OnPointerUp");
		_isDragging = false;
		_cardServices.EndCardDrag(this, eventData);
	}
}
