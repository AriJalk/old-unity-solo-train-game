using System;

namespace CardSystem
{
	public interface ICardDropTarget
	{
		public event Action<Guid> OnCardDropEvent;
		void OnDrop(CardObjectBase card);
		void SetActive(bool active);
	}
}
