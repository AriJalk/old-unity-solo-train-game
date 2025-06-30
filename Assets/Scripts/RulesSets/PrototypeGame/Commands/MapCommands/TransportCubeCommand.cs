using GameEngine.Commands;
using PrototypeGame.Events;
using PrototypeGame.Events.CommandRequestEvents;
using System;

namespace PrototypeGame.Commands
{
	internal class TransportCubeCommand : ICommand
	{
		private readonly MapCommandRequestEvents _mapCommandRequestEvents;
		private readonly Guid _origin;
		private readonly Guid _destination;

		public TransportCubeCommand(MapCommandRequestEvents mapCommandRequestEvents, Guid origin, Guid destination) 
		{ 
			_mapCommandRequestEvents = mapCommandRequestEvents;
			_origin = origin;
			_destination = destination;
		}
		public void Execute()
		{
			_mapCommandRequestEvents.RaiseTransportRequestEvent(_origin, _destination);
		}

		public void Undo()
		{
			_mapCommandRequestEvents.RaiseTransportRequestEvent(_destination, _origin);
		}
	}
}
