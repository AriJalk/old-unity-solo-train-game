using System;
using System.Collections.Generic;

namespace CardSystem
{
	internal class CardIndexTracker
	{
		private readonly Dictionary<Guid, Stack<int>> _cardIndexStacks = new Dictionary<Guid, Stack<int>>();

		public void RecordIndex(Guid cardId, int index)
		{
			if (!_cardIndexStacks.ContainsKey(cardId))
			{
				_cardIndexStacks.Add(cardId, new Stack<int>());
			}
			_cardIndexStacks[cardId].Push(index);
		}

		public int? PopLastIndex(Guid cardId)
		{
			if (_cardIndexStacks.TryGetValue(cardId, out var stack) && stack.Count > 0)
			{
				return stack.Pop();
			}
			return null;
		}
	}
}
