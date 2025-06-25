using GameEngine.Commands;
using HexSystem;
using System;

namespace PrototypeGame.Commands
{
	internal class CommandManager : CommandManagerBase
	{
		private LogicStateEvents _logicStateEvents;

		public CommandManager(LogicStateEvents logicStateEvents)
		{
			_logicStateEvents = logicStateEvents;
		}

		public void CreateAndExecuteTrasnportCommand(Guid originSlot, Guid destinationSlot)
		{
			TransportCubeCommand command = new TransportCubeCommand(_logicStateEvents, originSlot, destinationSlot);
			command.Execute();
			_commandGroupHead.AddCommand(command);
		}

		public void CreateAndExecuteFactoryBuildCommand(HexCoord hexCoord, GoodsColor productionColor)
		{
			BuildFactoryCommand command = new BuildFactoryCommand(_logicStateEvents, hexCoord, productionColor);
			command.Execute();
			_commandGroupHead.AddCommand(command);
		}
	}
}
