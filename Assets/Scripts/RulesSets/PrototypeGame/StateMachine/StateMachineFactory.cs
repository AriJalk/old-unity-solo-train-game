using CardSystem;
using GameEngine.Commands;
using PrototypeGame.Commands;
using PrototypeGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeGame.StateMachine
{
	internal class StateMachineFactory
	{
		private CommandManager _commandManager;
		private UserInterface _userInterface;
		private CardServices _cardServices;

		public StateMachineFactory(CommandManager commandManager, UserInterface userInterface)
		{
			_commandManager = commandManager;
			_userInterface = userInterface;
			_cardServices = new CardServices();
		}

		public AwaitingCardPlayState CreateAwatingCardPlayState()
		{
			AwaitingCardPlayState awaitingCardPlayState = new AwaitingCardPlayState(_cardServices, _userInterface);

			return awaitingCardPlayState;
		}
	}
}
