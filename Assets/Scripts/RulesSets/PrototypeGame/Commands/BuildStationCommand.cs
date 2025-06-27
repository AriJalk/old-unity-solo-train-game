using GameEngine.Commands;
using HexSystem;

namespace PrototypeGame.Commands
{
	internal class BuildStationCommand : ICommand
	{
		private readonly CommandRequestEvents _commandRequestEvents;
		private readonly HexCoord _hexCoord;

		public BuildStationCommand(CommandRequestEvents commandRequestEvents, HexCoord hexCoord)
		{
			_commandRequestEvents = commandRequestEvents;
			_hexCoord = hexCoord;
		}
		public void Execute()
		{
			_commandRequestEvents.RaiseBuildStationRequestEvent(_hexCoord);
		}

		public void Undo()
		{
			_commandRequestEvents.RaiseRemoveStationRequestEvent(_hexCoord);
		}
	}
}
