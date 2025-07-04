using CommonEngine.Core;
using CommonEngine.Interfaces;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardSystem
{
	/// <summary>
	/// The baseclass for visual representation of cards in the hand which can be dragged
	/// </summary>
	public class CardObjectBase : MonoBehaviour, IIdentifiable, IPointerDownHandler
	{
		public Guid guid { get; set; }
		public CardObjectServices CardServices { get; set; }
		public CommonServices CommonServices { get; set; }
		public RectTransform RectTransform { get; set; }
		public bool IsAlwaysLastInHand { get; set; }

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
				RectTransform.position = CommonServices.CurrentMousePosition;
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
				if (CommonServices.CurrentMouseDelta.magnitude > CardServices.MotionTreshold)
				{
					isStopped = true;
					break;
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
