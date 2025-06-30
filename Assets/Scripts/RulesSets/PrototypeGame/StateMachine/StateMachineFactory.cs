using CardSystem;
using CommonEngine.Core;
using GameEngine.Commands;
using PrototypeGame.Commands;
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
		private CardServices _cardServices;
		private ICardLookupService _cardLookupService;

		public StateMachineFactory()
		{

		}

		public void Initialize(CommandManager commandManager, UserInterface userInterface, CardServices cardServices, CommandFactory commandFactory, CommonServices commonServices, ICardLookupService cardLookupService)
		{
			_commandManager = commandManager;
			_userInterface = userInterface;
			_cardServices = cardServices;
			_commandFactory = commandFactory;
			_commonServices = commonServices;
			_cardLookupService = cardLookupService;
		}

		private CardDragAndDropState CreateCardDragAndDropState()
		{
			CardDragAndDropState state = new CardDragAndDropState(_cardServices, _userInterface.PlayCardDropTarget);

			return state;
		}

		public AwatingPlayCardForAction CreateAwatingPlayCardForAction()
		{
			AwatingPlayCardForAction state = new AwatingPlayCardForAction(_userInterface, _commandManager, _commandFactory, CreateCardDragAndDropState());

			return state;
		}

		public BuildActionState CreateBuildActionState(int availableMoney)
		{
			BuildActionState state = new BuildActionState(_commonServices, _commandManager, _commandFactory, _userInterface, CreateCardDragAndDropState(), _cardLookupService, availableMoney);

			return state;
		}


	}
}
