using CommonEngine.SceneServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardServices : MonoBehaviour
{
    [SerializeField]
    private Transform _dragLayer;
    [SerializeField]
    GraphicRaycaster _graphicRaycaster;
    [SerializeField]
    ScrollRect _scrollRect;

    private Transform _cardContainer;

    public void BeginCardDrag(CardInHandObject card)
    {
        _cardContainer = card.transform.parent;
        SceneHelpers.SetParentAndResetPosition(card.transform, _dragLayer);
        _scrollRect.enabled = false;
    }

    public void EndCardDrag(CardInHandObject card, PointerEventData pointerEventData)
    {
		SceneHelpers.SetParentAndResetPosition(card.transform, _cardContainer);
		_cardContainer = null;

		List<RaycastResult> results = new List<RaycastResult>();
		_graphicRaycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<CardDropArea>() is CardDropArea cardDropArea)
            {
                cardDropArea.OnDrop(card);
            }
        }
		_scrollRect.enabled = true;
	}

    public void OnDropArea(CardInHandObject card)
    {
        Debug.Log("Dropped: " + card.transform.parent.name);
        Destroy(card.transform.parent.gameObject);
    }

    public void ConsumeCard(CardInHandObject card)
    {

    }
}
