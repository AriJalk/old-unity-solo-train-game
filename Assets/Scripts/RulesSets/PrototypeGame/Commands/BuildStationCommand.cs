using GameEngine.Commands;
using HexSystem;

namespace PrototypeGame.Commands
{
	internal class BuildStationCommand : ICommand
	{
		private readonly LogicStateEvents _logicStateEvents;
		private readonly HexCoord _hexCoord;

		public BuildStationCommand(LogicStateEvents logicStateEvents, HexCoord hexCoord)
		{
			_logicStateEvents = logicStateEvents;
			_hexCoord = hexCoord;
		}
		public void Execute()
		{
			_logicStateEvents.RaiseBuildStationRequestEvent(_hexCoord);
		}

		public void Undo()
		{
			_logicStateEvents.RaiseRemoveStationRequestEvent(_hexCoord);
		}
	}
}
