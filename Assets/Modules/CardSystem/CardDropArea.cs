using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropArea : MonoBehaviour, IDropHandler
{
	[SerializeField]
	CardServices _cardServices;

	public void OnDrop(PointerEventData eventData)
	{
		var draggedObj = eventData.pointerDrag;
		if (draggedObj == null) return;

		var card = draggedObj.GetComponent<CardInHandObject>();
		if (card == null) return;

		_cardServices.OnDropArea(card);
	}

}
