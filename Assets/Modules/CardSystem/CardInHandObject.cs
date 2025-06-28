using CommonEngine.Core;
using CommonEngine.UI;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CardInHandObject : MonoBehaviour, IPointerDownHandler
{
	[SerializeField]
	private CardServices _cardServices;
	[SerializeField]
	private CommonServices _commonServices;

	[SerializeField]
	private float _dragDelay = 0.25f;
	[SerializeField]
	private float _motionTreshold = 2.5f;

	private bool _isDragging;

	private RectTransform _rectTransform;

	private Coroutine _delayCoroutine;

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
		//Debug.Log("OnPointerDown");
		_delayCoroutine = StartCoroutine(DragDelay());
		_commonServices.InputEvents.MouseButtonClickedUpEvent += OnMouseButtonUp;
	}

	private void StartDrag()
	{
		_isDragging = true;
		_cardServices.BeginCardDrag(this);
	}


	private void OnMouseButtonUp(int button, Vector2 position)
	{
		if (button == 0)
		{
			_commonServices.InputLock.RemoveLock(gameObject);
			//Debug.Log("OnPointerUp");
			if (_isDragging)
			{
				EndDrag(CreatePointerEvent(position));
			}
			else if (_delayCoroutine != null)
			{
				StopCoroutine(_delayCoroutine);
				_delayCoroutine = null;
			}
			_commonServices.InputEvents.MouseButtonClickedUpEvent -= OnMouseButtonUp;
		}
	}

	private void EndDrag(PointerEventData eventData)
	{
		_isDragging = false;
		_cardServices.EndCardDrag(this, eventData);
	}

	private PointerEventData CreatePointerEvent(Vector2 position)
	{
		return new PointerEventData(EventSystem.current)
		{
			position = position
		};
	}


	private IEnumerator DragDelay()
	{
		float _delayTime = _dragDelay;
		bool isStopped = false;
		while (_delayTime > 0)
		{
			if (Mouse.current.delta.magnitude > _motionTreshold)
			{
				isStopped = true;
			}
			_delayTime -= Time.deltaTime;
			yield return null;
		}
		if (!isStopped)
			StartDrag();
		_delayCoroutine = null;
	}
}
