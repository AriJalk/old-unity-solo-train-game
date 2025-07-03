using PrototypeGame.Events.CommandRequestEvents;
using System;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.Commands.CardCommands
{
	internal class PlayCardActionCommand : ICommand
	{
		private Guid _cardId;
		private CardCommandRequestEvents _cardCommandRequestEvents;
		public PlayCardActionCommand(CardCommandRequestEvents cardCommandRequestEvents, Guid cardId) 
		{ 
			_cardCommandRequestEvents = cardCommandRequestEvents;
			_cardId = cardId;
		}
		public void Execute()
		{
			_cardCommandRequestEvents.RaisePlayCardActionRequestEvent(_cardId);
		}

		public void Undo()
		{
			return;
		}
	}
}
