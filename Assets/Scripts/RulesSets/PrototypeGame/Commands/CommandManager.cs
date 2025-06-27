using GameEngine.Commands;
using HexSystem;
using System;

namespace PrototypeGame.Commands
{
	internal class CommandManager : CommandManagerBase
	{
		private CommandRequestEvents _commandRequestEvents;

		public CommandManager(CommandRequestEvents logicStateEvents)
		{
			_commandRequestEvents = logicStateEvents;
		}

		public void CreateAndExecuteTrasnportCommand(Guid originSlot, Guid destinationSlot)
		{
			TransportCubeCommand command = new TransportCubeCommand(_commandRequestEvents, originSlot, destinationSlot);
			command.Execute();
			_commandGroupHead.AddCommand(command);
		}

		public void CreateAndExecuteBuildFactoryCommand(HexCoord hexCoord, GoodsColor productionColor)
		{
			BuildFactoryCommand command = new BuildFactoryCommand(_commandRequestEvents, hexCoord, productionColor);
			command.Execute();
			_commandGroupHead.AddCommand(command);
		}

		public void CreateAndExecuteBuildStationCommand(HexCoord hexCoord)
		{
			BuildStationCommand command = new BuildStationCommand(_commandRequestEvents, hexCoord);
			command.Execute();
			_commandGroupHead.AddCommand(command);
		}

		public void CreateAndExecuteProduceGoodsCubeInSlotCommand(Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			ProduceGoodsCubeInSlotCommand command = new ProduceGoodsCubeInSlotCommand(_commandRequestEvents, goodsCubeSlotGuid, goodsColor);
			command.Execute();
			_commandGroupHead.AddCommand(command);
		}
	}
}
