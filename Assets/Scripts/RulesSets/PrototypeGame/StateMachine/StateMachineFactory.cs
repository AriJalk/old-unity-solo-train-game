using CardSystem;
using CommonEngine.Core;
using TurnBasedHexEngine.Commands;
using PrototypeGame.Commands;
using PrototypeGame.Events;
using PrototypeGame.Logic.Services;
using PrototypeGame.StateMachine.CommonStates;
using PrototypeGame.UI;


namespace PrototypeGame.StateMachine
{
	/// <summary>
	/// Factory class for all statemachine classes
	/// *** All state decision to be reflected on the game must be done through commands, not events ***
	/// </summary>
	internal class StateMachineFactory
	{
		private CommandManager _commandManager;
		private CommandFactory _commandFactory;

		private CommonServices _commonServices;
		private UserInterface _userInterface;
		private CardObjectServices _cardServices;
		private ICardLookupService _cardLookupService;

		/// <summary>
		/// *** Only for listening ***
		/// </summary>
		private CommandRequestEventsWrapper _commandRequestEventsWrapper;

		public StateMachineFactory()
		{

		}

		public void Initialize(CommandManager commandManager, UserInterface userInterface, CardObjectServices cardServices, CommandFactory commandFactory, CommonServices commonServices, ICardLookupService cardLookupService, CommandRequestEventsWrapper commandRequestEventsWrapper)
		{
			_commandManager = commandManager;
			_userInterface = userInterface;
			_cardServices = cardServices;
			_commandFactory = commandFactory;
			_commonServices = commonServices;
			_cardLookupService = cardLookupService;
			_commandRequestEventsWrapper = commandRequestEventsWrapper;
		}

		private CardDragAndDropState CreateCardDragAndDropState()
		{
			CardDragAndDropState state = new CardDragAndDropState(_cardServices, _userInterface.PlayCardDropTarget);

			return state;
		}

		public AwatingPlayCardForActionState CreateAwatingPlayCardForActionState()
		{
			AwatingPlayCardForActionState state = new AwatingPlayCardForActionState(_userInterface, _commandManager, _commandFactory, CreateCardDragAndDropState());

			return state;
		}

		public BuildActionState CreateBuildActionState(int availableMoney)
		{
			BuildActionState state = new BuildActionState(_commonServices, _commandManager, _commandFactory, _userInterface, CreateCardDragAndDropState(), _cardLookupService, _commandRequestEventsWrapper.CardCommandRequestEvents, availableMoney);

			return state;
		}


	}
}
