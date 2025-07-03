using HexSystem;
using PrototypeGame.Events.CommandRequestEvents;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.Commands
{
	internal class BuildStationCommand : ICommand
	{
		private readonly MapCommandRequestEvents _mapCommandRequestEvents;
		private readonly HexCoord _hexCoord;

		public BuildStationCommand(MapCommandRequestEvents mapCommandRequestEvents, HexCoord hexCoord)
		{
			_mapCommandRequestEvents = mapCommandRequestEvents;
			_hexCoord = hexCoord;
		}
		public void Execute()
		{
			_mapCommandRequestEvents.RaiseBuildStationRequestEvent(_hexCoord);
		}

		public void Undo()
		{
			_mapCommandRequestEvents.RaiseRemoveStationRequestEvent(_hexCoord);
		}
	}
}
