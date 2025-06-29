using UnityEngine;

namespace CardSystem
{
	public class ConsumeCardDropArea : MonoBehaviour
	{
		[SerializeField]
		CardServices _cardServices;

		public void OnDrop(CardInHandObject card)
		{
			if (card != null)
			{
				_cardServices.RemoveCard(card);
			}
		}
	}

}
