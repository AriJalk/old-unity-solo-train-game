using CardSystem;
using PrototypeGame.Scene.Components.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PrototypeGame.UI.CardSystem
{
	public class PlayCardDropArea : MonoBehaviour, ICardDropArea
	{
		public event Action<Guid> OnCardDropEvent;

		public void OnDrop(CardObjectBase card)
		{
			ProtoCardObject protoCard = card.GetComponent<ProtoCardObject>();
			OnCardDropEvent?.Invoke(protoCard.guid);
		}
	}
}
