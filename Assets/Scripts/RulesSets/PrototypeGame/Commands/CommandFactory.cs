using HexSystem;
using PrototypeGame.Events;
using System;

namespace PrototypeGame.Commands
{
	internal class CommandFactory
	{
		private CommandRequestEvents _commandRequestEvents;

		public CommandFactory(CommandRequestEvents commandRequestEvents)
		{
			_commandRequestEvents = commandRequestEvents;
		}

		public void CreateTrasnportCommand(Guid originSlot, Guid destinationSlot)
		{
			TransportCubeCommand command = new TransportCubeCommand(_commandRequestEvents, originSlot, destinationSlot);
		}

		public void CreateBuildFactoryCommand(HexCoord hexCoord, GoodsColor productionColor)
		{
			BuildFactoryCommand command = new BuildFactoryCommand(_commandRequestEvents, hexCoord, productionColor);
		}

		public void CreateBuildStationCommand(HexCoord hexCoord)
		{
			BuildStationCommand command = new BuildStationCommand(_commandRequestEvents, hexCoord);
		}

		public void CreateProduceGoodsCubeInSlotCommand(Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			ProduceGoodsCubeInSlotCommand command = new ProduceGoodsCubeInSlotCommand(_commandRequestEvents, goodsCubeSlotGuid, goodsColor);
		}
	}
}
