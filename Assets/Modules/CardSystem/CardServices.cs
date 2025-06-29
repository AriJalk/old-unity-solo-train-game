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
			GameObject cardPrefab = Resources.Load<GameObject>("Prefabs/Card");

			for (int i = 0; i < 7; i++)
			{
				CardInHandObject card = GameObject.Instantiate(cardPrefab).GetComponent<CardInHandObject>();
				card.CardServices = this;
				card.CommonServices = _commonServices;
				AddCard(card);
			}
		}

		public void BeginCardDrag(CardInHandObject card)
		{
			_cardContainer = card.transform.parent.GetComponent<RectTransform>();
			SceneHelpers.SetParentAndResetPosition(card.RectTransform, _dragLayer);
			_scrollRect.enabled = false;
		}

		public void EndCardDrag(CardInHandObject card, PointerEventData pointerEventData)
		{
			// Return card to hand container
			SceneHelpers.SetParentAndResetPosition(card.RectTransform, _cardContainer);
			_cardContainer = null;

			// Raycast for drop area

			List<RaycastResult> results = new List<RaycastResult>();
			_graphicRaycaster.Raycast(pointerEventData, results);

			foreach (RaycastResult result in results)
			{
				if (result.gameObject.GetComponent<ConsumeCardDropArea>() is ConsumeCardDropArea cardDropArea)
				{
					cardDropArea.OnDrop(card);
					break;
				}
			}
			_scrollRect.enabled = true;
		}

		public void AddCard(CardInHandObject card)
		{
			RectTransform container = new GameObject("CardContainer").AddComponent<RectTransform>();
			container.sizeDelta = new Vector2(100, 150);
			container.SetParent(_cardHandTransform, false);
			SceneHelpers.InitializeRectObject(card.RectTransform, container);
		}


		public void RemoveCard(CardInHandObject card)
		{
			Debug.Log(card.transform.parent.name);
			Destroy(card.transform.parent.gameObject);
		}
	}
}
