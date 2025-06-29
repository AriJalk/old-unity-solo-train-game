using CardSystem;
using CommonEngine.Core;
using GameEngine.Commands;
using PrototypeGame.Commands;
using PrototypeGame.Events;
using PrototypeGame.UI;


namespace PrototypeGame.StateMachine
{
	internal class StateMachineFactory
	{
		private CommandManager _commandManager;
		private CommandRequestEvents _requestEvents;
		private CommandFactory _commandFactory;


		private CommonServices _commonServices;
		private UserInterface _userInterface;
		private CardServices _cardServices;

		public StateMachineFactory()
		{

		}

		public void Initialize(CommandManager commandManager, CommandRequestEvents commandRequestEvents, UserInterface userInterface, CardServices cardServices, CommandFactory commandFactory, CommonServices commonServices)
		{
			_commandManager = commandManager;
			_requestEvents = commandRequestEvents;
			_userInterface = userInterface;
			_cardServices = cardServices;
			_commandFactory = commandFactory;
			_commonServices = commonServices;
		}

		public AwaitingCardPlayState CreateAwatingCardPlayState()
		{
			AwaitingCardPlayState state = new AwaitingCardPlayState(_cardServices, _userInterface, _commandManager, _commandFactory);

			return state;
		}

		public BuildActionState CreateBuildActionState(int availableMoney)
		{
			BuildActionState state = new BuildActionState(availableMoney, _commonServices, _userInterface);

			return state;
		}

	}
}
