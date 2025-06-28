using CommonEngine.SceneServices;
using UnityEngine;

public class CardServices : MonoBehaviour
{
    [SerializeField]
    private Transform _dragLayer;

    private Transform _draggedCardTransform;

    public void BeginCardDrag(CardInHandObject card)
    {
        _draggedCardTransform = card.transform;
        SceneHelpers.SetParentAndResetPosition(card.PanelRectTransform, _dragLayer);
    }

    public void EndCardDrag(CardInHandObject card)
    {
        SceneHelpers.SetParentAndResetPosition(card.PanelRectTransform, _draggedCardTransform);
        _draggedCardTransform = null;
	}

    public void OnDropArea(CardInHandObject card)
    {
        Debug.Log("Dropped: " + card.name);
        Destroy(card.PanelRectTransform.gameObject);
        Destroy(card.gameObject);
    }
}
