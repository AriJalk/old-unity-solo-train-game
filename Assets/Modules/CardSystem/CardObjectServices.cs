using CommonEngine.Core;
using CommonEngine.SceneServices;
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

		public float DragDelay = 0.25f;
		public float MotionTreshold = 2.5f;

		private RectTransform _cardContainer;

		public void BeginCardDrag(CardObjectBase card)
		{
			_cardContainer = card.transform.parent.GetComponent<RectTransform>();
			card.RectTransform.SetParent(_dragLayer);
			//SceneHelpers.SetParentAndResetPosition(card.RectTransform, _dragLayer);
			_scrollRect.enabled = false;
			DragStartedEvent?.Invoke();
		}

		public void EndCardDrag(CardObjectBase card, PointerEventData pointerEventData)
		{
			// Return card to hand container
			SceneHelpers.SetParentAndResetPosition(card.RectTransform, _cardContainer);
			_cardContainer = null;

			// Raycast for drop area

			List<RaycastResult> results = new List<RaycastResult>();
			_graphicRaycaster.Raycast(pointerEventData, results);

			foreach (RaycastResult result in results)
			{
				if (result.gameObject.GetComponent<ICardDropTarget>() is ICardDropTarget cardDropArea)
				{
					cardDropArea.OnDrop(card);
					break;
				}
			}
			_scrollRect.enabled = true;
			DragEndedEvent?.Invoke();
		}

		public void AddCard(CardObjectBase card, bool fromUndo = false)
		{
			RectTransform container = new GameObject("CardContainer").AddComponent<RectTransform>();
			container.SetParent(_cardHandTransform, false);
			if (fromUndo)
			{
				container.SetSiblingIndex(card.IndexAtHand);
			}
			else
			{
				card.IndexAtHand = container.GetSiblingIndex();
			}
			SceneHelpers.InitializeRectObject(card.RectTransform, container);
			float cardWidth = _cardHandTransform.rect.height * CARD_ASPECT_RATIO;
			float cardHeight = _cardHandTransform.rect.height;
			container.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardWidth);
			container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cardHeight);
		}


		public void RemoveCard(CardObjectBase card)
		{
			//Debug.Log("Removed: " + card.transform.parent.name);
			Destroy(card.transform.parent.gameObject);
		}
	}
}
