using UnityEngine;

namespace CardSystem
{
	public class ConsumeCardDropArea : MonoBehaviour, ICardDropArea
	{
		[SerializeField]
		CardServices _cardServices;

		public void OnDrop(CardObjectBase card)
		{
			if (card != null)
			{
				_cardServices.RemoveCard(card);
			}
		}
	}

}
