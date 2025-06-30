using Commands.StateCommands;
using TurnBasedHexEngine.StateMachine;
using HexSystem;
using PrototypeGame.Commands.CardCommands;
using PrototypeGame.Events;
using PrototypeGame.StateMachine;
using System;

namespace PrototypeGame.Commands
{
	/// <summary>
	/// Factory class for all commands
	/// *** All commands must interact with the game state through request event calls, or transition states through direct access to StateMachineManager ***
	/// </summary>
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

		public TransportCubeCommand CreateTrasnportCommand(Guid originSlot, Guid destinationSlot)
		{
			TransportCubeCommand command = new TransportCubeCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, originSlot, destinationSlot);
			return command;
		}

		public BuildFactoryCommand CreateBuildFactoryCommand(HexCoord hexCoord, GoodsColor productionColor)
		{
			BuildFactoryCommand command = new BuildFactoryCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, hexCoord, productionColor);
			return command;
		}

		public BuildStationCommand CreateBuildStationCommand(HexCoord hexCoord)
		{
			BuildStationCommand command = new BuildStationCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, hexCoord);
			return command;
		}

		public ProduceGoodsCubeInSlotCommand CreateProduceGoodsCubeInSlotCommand(Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			ProduceGoodsCubeInSlotCommand command = new ProduceGoodsCubeInSlotCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, goodsCubeSlotGuid, goodsColor);
			return command;
		}

		public TransitionToBuildStateCommand CreateTransitionToBuildStateCommand(int availableMoney)
		{
			TransitionToBuildStateCommand command = new TransitionToBuildStateCommand(_commandRequestEventsWrapper.StateCommandRequestEvents, _stateMachineFactory, availableMoney, _stateMachineManager.CurrentState); 

			return command;
		}

		public TransitionToAwatingPlayCardForActionCommand CreateTransitionToAwatingPlayCardForActionCommand()
		{
			TransitionToAwatingPlayCardForActionCommand command = new TransitionToAwatingPlayCardForActionCommand(_commandRequestEventsWrapper.StateCommandRequestEvents, _stateMachineFactory, _stateMachineManager.CurrentState);

			return command;
		}


		public PlayCardActionCommand CreatePlayCardActionCommand(Guid cardId)
		{
			PlayCardActionCommand command = new PlayCardActionCommand(_commandRequestEventsWrapper.CardCommandRequestEvents, cardId);

			return command;
		}

		public RemoveCardFromHandCommand CreateRemoveCardFromHandCommand(Guid cardId)
		{
			RemoveCardFromHandCommand command = new RemoveCardFromHandCommand(_commandRequestEventsWrapper.CardCommandRequestEvents, cardId);

			return command;
		}
	}
}
