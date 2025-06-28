using CommonEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInHandObject : LockingUIPanel, IDragHandler, IBeginDragHandler, IEndDragHandler
{

	private static Vector2 AnchorMinIdle = new Vector2(0, -0.15f);
	private static Vector2 AnchorMaxIdle = new Vector2(1, 0.85f);

	private static Vector2 AnchorMinHover = new Vector2(0, 0);
	private static Vector2 AnchorMaxHover = new Vector2(1, 1);

	[SerializeField]
	private CardServices _cardServices;

	public RectTransform PanelRectTransform;

	public void OnBeginDrag(PointerEventData eventData)
	{
		_cardServices.BeginCardDrag(this);
	}

	public void OnDrag(PointerEventData eventData)
	{
		PanelRectTransform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_cardServices.EndCardDrag(this);
		PanelRectTransform.anchorMin = CardInHandObject.AnchorMinIdle;
		PanelRectTransform.anchorMax = CardInHandObject.AnchorMaxIdle;
		PanelRectTransform.anchoredPosition = Vector2.zero;
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		PanelRectTransform.anchorMin = AnchorMinHover;
		PanelRectTransform.anchorMax = AnchorMaxHover;
		
	}

	public override void OnPointerExit(PointerEventData eventData) {
		base.OnPointerExit(eventData);
		PanelRectTransform.anchorMin = AnchorMinIdle;
		PanelRectTransform.anchorMax = AnchorMaxIdle;
	}
}
