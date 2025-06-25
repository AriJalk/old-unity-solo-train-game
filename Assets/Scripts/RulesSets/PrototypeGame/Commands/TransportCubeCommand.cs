using GameEngine.Commands;
using System;

namespace PrototypeGame.Commands
{
	internal class TransportCubeCommand : ICommand
	{
		private readonly LogicStateEvents _logicStateEvents;
		private readonly Guid _origin;
		private readonly Guid _destination;

		public TransportCubeCommand(LogicStateEvents logicStateEvents, Guid origin, Guid destination) 
		{ 
			_logicStateEvents = logicStateEvents;
			_origin = origin;
			_destination = destination;
		}
		public void Execute()
		{
			_logicStateEvents.RaiseTransportRequestEvent(_origin, _destination);
		}

		public void Undo()
		{
			_logicStateEvents.RaiseTransportRequestEvent(_destination, _origin);
		}
	}
}
