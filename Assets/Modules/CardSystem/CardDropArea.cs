using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropArea : MonoBehaviour
{
	[SerializeField]
	CardServices _cardServices;

	public void OnDrop(CardInHandObject card)
	{
		if (card != null)
		{
			_cardServices.OnDropArea(card);
		}
	}

}
