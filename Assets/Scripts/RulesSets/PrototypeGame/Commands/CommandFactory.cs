using Commands.StateCommands;
using GameEngine.StateMachine;
using HexSystem;
using PrototypeGame.Events;
using PrototypeGame.StateMachine;
using System;

namespace PrototypeGame.Commands
{
	internal class CommandFactory
	{
		private CommandRequestEventsWrapper _commandRequestEventsWrapper;
		private StateMachineFactory _stateMachineFactory;
		private StateMachineManager _stateMachineManager;

		public CommandFactory()
		{

		}

		public void Initialize(CommandRequestEventsWrapper commandRequestEventsWrapper, StateMachineFactory stateMachineFactory, StateMachineManager stateMachineManager)
		{
			_commandRequestEventsWrapper = commandRequestEventsWrapper;
			_stateMachineFactory = stateMachineFactory;
			_stateMachineManager = stateMachineManager;
		}

		public void CreateTrasnportCommand(Guid originSlot, Guid destinationSlot)
		{
			TransportCubeCommand command = new TransportCubeCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, originSlot, destinationSlot);
		}

		public void CreateBuildFactoryCommand(HexCoord hexCoord, GoodsColor productionColor)
		{
			BuildFactoryCommand command = new BuildFactoryCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, hexCoord, productionColor);
		}

		public void CreateBuildStationCommand(HexCoord hexCoord)
		{
			BuildStationCommand command = new BuildStationCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, hexCoord);
		}

		public void CreateProduceGoodsCubeInSlotCommand(Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			ProduceGoodsCubeInSlotCommand command = new ProduceGoodsCubeInSlotCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, goodsCubeSlotGuid, goodsColor);
		}

		public TransitionToBuildStateCommand CreateTransitionToBuildStateCommand(int availableMoney)
		{
			TransitionToBuildStateCommand command = new TransitionToBuildStateCommand(_commandRequestEventsWrapper.StateCommandRequestEvents, _stateMachineFactory, availableMoney, _stateMachineManager.CurrentState); 

			return command;
		}
		
		public PlayCardActionCommand CreatePlayCardActionCommand(Guid cardId)
		{
			PlayCardActionCommand command = new PlayCardActionCommand(_commandRequestEventsWrapper.CardCommandRequestEvents, cardId);

			return command;
		}
	}
}
