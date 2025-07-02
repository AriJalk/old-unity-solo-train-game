using Assets.Scripts.RulesSets.PrototypeGame.Commands.CardCommands;
using Commands.StateCommands;
using HexSystem;
using PrototypeGame.Commands.CardCommands;
using PrototypeGame.Commands.StateCommands;
using PrototypeGame.Events;
using PrototypeGame.Logic.ServiceContracts;
using PrototypeGame.ServiceGroups;
using PrototypeGame.StateMachine;
using System;
using TurnBasedHexEngine.StateMachine;

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
		private ICardLookupService _cardLookupService;
		private IFactoryLookupService _factoryLookupService;

		public CommandFactory()
		{

		}

		public void Initialize(CommandRequestEventsWrapper commandRequestEventsWrapper,
				StateMachineFactory stateMachineFactory,
				GameStateManagers gameStateManagers,
				SceneEventsWrapper sceneEventsWrapper)
		{
			_commandRequestEventsWrapper = commandRequestEventsWrapper;
			_stateMachineFactory = stateMachineFactory;
			_stateMachineManager = gameStateManagers.StateMachineManager;
			_cardLookupService = gameStateManagers.LogicCardStateManager;
			_factoryLookupService = gameStateManagers.LogicMapStateManager;
		}

		public TransportCubeCommand CreateTransportCubeCommand(Guid originSlot, Guid destinationSlot)
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

		public TransitionToTransportStateCommand CreateTransitionToTransportStateCommand(int transportPoints)
		{
			TransitionToTransportStateCommand command = new TransitionToTransportStateCommand(_commandRequestEventsWrapper.StateCommandRequestEvents, _stateMachineFactory, transportPoints, _stateMachineManager.CurrentState);

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

		public RetrieveCardsFromDiscardCommand CreateRetreiveCardsFromDiscardCommand()
		{
			RetrieveCardsFromDiscardCommand command = new RetrieveCardsFromDiscardCommand(_commandRequestEventsWrapper.CardCommandRequestEvents, _cardLookupService.GetCardsInDiscardPile());

			return command;
		}

		public ProduceGoodsInAllFactorySlotsCommand CreateProduceGoodsInAllFactorySlotsCommand()
		{
			ProduceGoodsInAllFactorySlotsCommand command = new ProduceGoodsInAllFactorySlotsCommand(_commandRequestEventsWrapper.MapCommandRequestEvents, _factoryLookupService.GetEmptyFactories());

			return command;
		}
	}
}
