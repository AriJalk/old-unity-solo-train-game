using GameEngine.Commands;
using PrototypeGame.Events.CommandRequestEvents;
using System;

namespace PrototypeGame.Commands.CardCommands
{
	internal class RemoveCardFromHandCommand : ICommand
	{
		private Guid _cardId;
		private CardCommandRequestEvents _cardCommandRequestEvents;
		public RemoveCardFromHandCommand(CardCommandRequestEvents cardCommandRequestEvents, Guid cardId)
		{
			_cardCommandRequestEvents = cardCommandRequestEvents;
			_cardId = cardId;
		}
		public void Execute()
		{
			_cardCommandRequestEvents.RaiseMoveCardFromHandToDiscardRequestEvent(_cardId);
		}

		public void Undo()
		{
			_cardCommandRequestEvents.RaiseMoveCardFromDiscardToHandRequestEvent(_cardId);
		}
	}
}
