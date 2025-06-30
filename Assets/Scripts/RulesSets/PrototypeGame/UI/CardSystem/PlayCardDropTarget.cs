using CardSystem;
using PrototypeGame.Scene.Components.Cards;
using System;
using UnityEngine;

namespace PrototypeGame.UI.CardSystem
{
	public class PlayCardDropTarget : MonoBehaviour, ICardDropTarget
	{
		public event Action<Guid> OnCardDropEvent;

		public void OnDrop(CardObjectBase card)
		{
			ProtoCardObject protoCard = card.GetComponent<ProtoCardObject>();
			OnCardDropEvent?.Invoke(protoCard.guid);
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}
