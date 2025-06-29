using CardSystem;
using GameEngine.Commands;
using PrototypeGame.StateMachine;
using PrototypeGame.UI;

namespace GameEngine.StateMachine
{
	internal class StateMachineFactory
	{
		private CommandManager _commandManager;
		private UserInterface _userInterface;
		private CardServices _cardServices;

		public StateMachineFactory(CommandManager commandManager, UserInterface userInterface, CardServices cardServices)
		{
			_commandManager = commandManager;
			_userInterface = userInterface;
			_cardServices = cardServices;
		}

		public AwaitingCardPlayState CreateAwatingCardPlayState()
		{
			AwaitingCardPlayState awaitingCardPlayState = new AwaitingCardPlayState(_cardServices, _userInterface);

			return awaitingCardPlayState;
		}
	}
}
