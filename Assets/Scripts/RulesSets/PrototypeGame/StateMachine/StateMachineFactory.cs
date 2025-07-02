using CardSystem;
using CommonEngine.Core;
using PrototypeGame.Commands;
using PrototypeGame.Events;
using PrototypeGame.Logic.ServiceContracts;
using PrototypeGame.RulesServices;
using PrototypeGame.ServiceGroups;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.StateMachine.StateServices;
using PrototypeGame.UI;
using TurnBasedHexEngine.Commands;


namespace PrototypeGame.StateMachine
{
	/// <summary>
	/// Factory class for all statemachine classes
	/// *** All state decision to be reflected on the game must be done through commands, not events ***
	/// </summary>
	internal class StateMachineFactory
	{
		private CoreStateDependencies _coreStateDependencies;

		private ICardLookupService _cardLookupService;
		private CardObjectServices _cardObjectServices;

		/// <summary>
		/// *** Only for listening ***
		/// </summary>
		private CommandRequestEventsWrapper _commandRequestEventsWrapper;

		public StateMachineFactory()
		{

		}

		public void Initialize(
			CommonServices commonServices, 
			UserInterface userInterface,
			CommandManager commandManager,
			CommandFactory commandFactory, 
			CardObjectServices cardServices,
			GameStateManagers gameStateManagers,
			CommandRequestEventsWrapper commandRequestEventsWrapper)
		{
			_cardObjectServices = cardServices;
			_cardLookupService = gameStateManagers.LogicCardStateManager; // or wherever ICardLookupService lives
			_commandRequestEventsWrapper = commandRequestEventsWrapper;
			_coreStateDependencies = new CoreStateDependencies(
				commonServices,
				userInterface,
				commandManager,
				commandFactory,
				new RulesValidator(gameStateManagers.LogicCardStateManager.LogicCardState, gameStateManagers.LogicMapStateManager.LogicMapState)
			);
		}

		private CardDragAndDropState CreateCardDragAndDropState()
		{
			CardDragAndDropState state = new CardDragAndDropState(_cardObjectServices, _coreStateDependencies.UserInterface.PlayCardDropTarget);

			return state;
		}

		public AwatingPlayCardForActionState CreateAwatingPlayCardForActionState()
		{
			AwatingPlayCardForActionState state = new AwatingPlayCardForActionState(_coreStateDependencies, CreateCardDragAndDropState());

			return state;
		}

		public BuildActionState CreateBuildActionState(int availableMoney)
		{
			BuildActionState state = new BuildActionState(_coreStateDependencies, CreateCardDragAndDropState(), _cardLookupService, _commandRequestEventsWrapper.CardCommandRequestEvents, availableMoney);

			return state;
		}

		public TransportActionState CreateTransportActionState(int transportPoints)
		{
			TransportActionState state = new TransportActionState(_coreStateDependencies, CreateCardDragAndDropState(), _cardLookupService, _commandRequestEventsWrapper.CardCommandRequestEvents, transportPoints);

			return state;
		}


	}
}
