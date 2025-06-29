using CommonEngine.Core;
using CommonEngine.SceneServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace CardSystem
{
	public class CardServices : MonoBehaviour
	{
		[SerializeField]
		private CommonServices _commonServices;
		[SerializeField]
		private RectTransform _dragLayer;
		[SerializeField]
		private GraphicRaycaster _graphicRaycaster;
		[SerializeField]
		private ScrollRect _scrollRect;
		[SerializeField]
		private Transform _cardHandTransform;

		public float DragDelay = 0.25f;
		public float MotionTreshold = 2.5f;

		private RectTransform _cardContainer;

		private void Start()
		{

		}

		public void BeginCardDrag(CardObjectBase card)
		{
			_cardContainer = card.transform.parent.GetComponent<RectTransform>();
			SceneHelpers.SetParentAndResetPosition(card.RectTransform, _dragLayer);
			_scrollRect.enabled = false;
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
				if (result.gameObject.GetComponent<ICardDropArea>() is ICardDropArea cardDropArea)
				{
					cardDropArea.OnDrop(card);
					break;
				}
			}
			_scrollRect.enabled = true;
		}

		public void AddCard(CardObjectBase card)
		{
			RectTransform container = new GameObject("CardContainer").AddComponent<RectTransform>();
			container.sizeDelta = new Vector2(100, 150);
			container.SetParent(_cardHandTransform, false);
			SceneHelpers.InitializeRectObject(card.RectTransform, container);
		}


		public void RemoveCard(CardObjectBase card)
		{
			Debug.Log("Removed: " + card.transform.parent.name);
			Destroy(card.transform.parent.gameObject);
		}
	}
}
