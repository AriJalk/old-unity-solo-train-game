using CommonEngine.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace CardSystem
{
	public class CardInHandObject : MonoBehaviour, IPointerDownHandler
	{
		public CardServices CardServices { get; set; }
		public CommonServices CommonServices { get; set; }

		public RectTransform RectTransform;


		private bool _isDragging;


		private Coroutine _delayCoroutine;

		private void Awake()
		{
			RectTransform = GetComponent<RectTransform>();
		}

		private void Update()
		{
			if (_isDragging)
			{
				RectTransform.position = Mouse.current.position.ReadValue();
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			CommonServices.InputLock.AddLock(gameObject);
			//Debug.Log("OnPointerDown");
			_delayCoroutine = StartCoroutine(DragDelay());
			CommonServices.InputEvents.MouseButtonClickedUpEvent += OnMouseButtonUp;
		}

		private void StartDrag()
		{
			_isDragging = true;
			CardServices.BeginCardDrag(this);
		}


		private void OnMouseButtonUp(int button, Vector2 position)
		{
			if (button == 0)
			{
				CommonServices.InputLock.RemoveLock(gameObject);
				CommonServices.InputEvents.MouseButtonClickedUpEvent -= OnMouseButtonUp;
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
			}
		}

		private void EndDrag(PointerEventData eventData)
		{
			_isDragging = false;
			CardServices.EndCardDrag(this, eventData);
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
			float _delayTime = CardServices.DragDelay;
			bool isStopped = false;
			while (_delayTime > 0)
			{
				if (Mouse.current.delta.magnitude > CardServices.MotionTreshold)
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
}
