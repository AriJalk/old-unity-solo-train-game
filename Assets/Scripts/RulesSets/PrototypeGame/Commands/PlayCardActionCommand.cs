using GameEngine.Commands;
using PrototypeGame.Events;
using System;

namespace PrototypeGame.Commands
{
	internal class PlayCardActionCommand : ICommand
	{
		private Guid _cardId;
		private CommandRequestEvents _commandRequestEvents;
		public PlayCardActionCommand(Guid cardId, CommandRequestEvents commandRequestEvents) 
		{ 
			_cardId = cardId;
			_commandRequestEvents = commandRequestEvents;
		}
		public void Execute()
		{
			_commandRequestEvents.RaisePlayCardActionRequestEvent(_cardId);
		}

		public void Undo()
		{
			return;
		}
	}
}
