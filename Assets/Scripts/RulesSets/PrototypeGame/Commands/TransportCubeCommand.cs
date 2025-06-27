using GameEngine.Commands;
using System;

namespace PrototypeGame.Commands
{
	internal class TransportCubeCommand : ICommand
	{
		private readonly CommandRequestEvents _commandRequestEvents;
		private readonly Guid _origin;
		private readonly Guid _destination;

		public TransportCubeCommand(CommandRequestEvents commandRequestEvents, Guid origin, Guid destination) 
		{ 
			_commandRequestEvents = commandRequestEvents;
			_origin = origin;
			_destination = destination;
		}
		public void Execute()
		{
			_commandRequestEvents.RaiseTransportRequestEvent(_origin, _destination);
		}

		public void Undo()
		{
			_commandRequestEvents.RaiseTransportRequestEvent(_destination, _origin);
		}
	}
}
