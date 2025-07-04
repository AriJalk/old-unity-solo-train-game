using System;

namespace CardSystem
{
	/// <summary>
	/// A drop target for cards
	/// </summary>
	public interface ICardDropTarget
	{
		public event Action<Guid> OnCardDropEvent;
		void OnDrop(CardObjectBase card);
		void SetActive(bool active);
	}
}
