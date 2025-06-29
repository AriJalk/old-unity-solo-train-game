using Commands.StateCommands;
using HexSystem;
using PrototypeGame.Events;
using PrototypeGame.StateMachine;
using System;

namespace PrototypeGame.Commands
{
	internal class CommandFactory
	{
		private CommandRequestEvents _commandRequestEvents;
		private StateMachineFactory _stateMachineFactory;

		public CommandFactory()
		{

		}

		public void Initialize(CommandRequestEvents commandRequestEvents, StateMachineFactory stateMachineFactory)
		{
			_commandRequestEvents = commandRequestEvents;
			_stateMachineFactory = stateMachineFactory;
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

		public TransitionToBuildStateCommand CreateTransitionToBuildStateCommand(int availableMoney)
		{
			TransitionToBuildStateCommand command = new TransitionToBuildStateCommand(availableMoney, _commandRequestEvents, _stateMachineFactory); 

			return command;
		}
		
		public PlayCardActionCommand CreatePlayCardActionCommand(Guid cardId)
		{
			PlayCardActionCommand command = new PlayCardActionCommand(cardId, _commandRequestEvents);

			return command;
		}
	}
}
