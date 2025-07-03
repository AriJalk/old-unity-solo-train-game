using CommonEngine.Core;
using CommonEngine.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardSystem
{
	public class CardObjectServices : MonoBehaviour
	{
		const float CARD_ASPECT_RATIO = 100 / 150f;
		public event Action DragStartedEvent;
		public event Action DragEndedEvent;

		[SerializeField]
		private CommonServices _commonServices;
		[SerializeField]
		private RectTransform _dragLayer;
		[SerializeField]
		private GraphicRaycaster _graphicRaycaster;
		[SerializeField]
		private ScrollRect _scrollRect;
		[SerializeField]
		private RectTransform _cardHandTransform;

		// Exposed properties
		public float DragDelay = 0.25f;
		public float MotionTreshold = 2.5f;


		private RectTransform _currentDraggedCardContainer;

		private CardIndexTracker _indexTracker;

		private HashSet<CardObjectBase> _lastCardsInHand;

		private void Awake()
		{
			_indexTracker = new CardIndexTracker();
			_lastCardsInHand = new HashSet<CardObjectBase>();
		}


		private void CheckForDropTarget(CardObjectBase card, PointerEventData pointerEventData)
		{
			// Raycast for drop area
			List<RaycastResult> results = new List<RaycastResult>();
			_graphicRaycaster.Raycast(pointerEventData, results);

			foreach (RaycastResult result in results)
			{
				if (result.gameObject.GetComponent<ICardDropTarget>() is ICardDropTarget cardDropArea)
				{
					cardDropArea.OnDrop(card);
					return;
				}
			}
		}

		public void BeginCardDrag(CardObjectBase card)
		{
			_currentDraggedCardContainer = card.transform.parent.GetComponent<RectTransform>();
			card.RectTransform.SetParent(_dragLayer);
			_scrollRect.enabled = false;
			DragStartedEvent?.Invoke();
		}

		public void EndCardDrag(CardObjectBase card, PointerEventData pointerEventData)
		{
			// Return card to hand container
			SceneHelpers.SetParentAndResetPosition(card.RectTransform, _currentDraggedCardContainer);
			_currentDraggedCardContainer = null;
			CheckForDropTarget(card, pointerEventData);
			_scrollRect.enabled = true;
			DragEndedEvent?.Invoke();
		}


		public void AddCard(CardObjectBase card, bool fromUndo)
		{
			RectTransform container = new GameObject("CardContainer").AddComponent<RectTransform>();
			container.SetParent(_cardHandTransform, false);
			if (fromUndo && _indexTracker.PopLastIndex(card.guid) is int index)
			{
				container.SetSiblingIndex(index);
			}
			SceneHelpers.InitializeRectObject(card.RectTransform, container);
			float cardWidth = _cardHandTransform.rect.height * CARD_ASPECT_RATIO;
			float cardHeight = _cardHandTransform.rect.height;
			container.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardWidth);
			container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cardHeight);
			if (card.IsAlwaysLastInHand)
			{
				_lastCardsInHand.Add(card);
			}
		}


		public void RemoveCard(CardObjectBase card, bool fromUndo)
		{
			if (card.IsAlwaysLastInHand)
			{
				_lastCardsInHand.Remove(card);
			}
			if (!fromUndo)
			{
				_indexTracker.RecordIndex(card.guid, card.transform.parent.GetSiblingIndex());
			}
			//Debug.Log("Removed: " + card.transform.parent.name);
			Destroy(card.transform.parent.gameObject);
		}

		/// <summary>
		/// Ensures last cards in hand stay at end
		/// </summary>
		public void ReorganaizeHand()
		{
			foreach (CardObjectBase card in _lastCardsInHand)
			{
				card.transform.parent.SetAsLastSibling();
			}
		}

	}
}
