using CardSystem;
using PrototypeGame.Commands;
using PrototypeGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeGame.StateMachine
{
	internal class StateMachineManager
	{
		//TODO: maybe get rid of this? hold only 1?
		private Stack<IStateMachine> _states;

		private CommandManager _commandManager;
		private UserInterface _userInterface;
		private CardServices _cardServices;

		public StateMachineManager(CommandManager commandManager, UserInterface userInterface, CardServices cardServices)
		{
			_states = new Stack<IStateMachine>();
			_commandManager = commandManager;
			_userInterface = userInterface;
			_cardServices = cardServices;
		}

		public void NextState(IStateMachine nextState)
		{
			ExitHeadState();
			nextState.EnterState();
			_states.Push(nextState);
		}

		public void ExitHeadState()
		{
			if (_states.Count > 0)
			{
				_states.Peek().ExitState();
			}
		}


		public AwaitingCardPlayState CreateAndEnterAwatingCardPlayState()
		{
			AwaitingCardPlayState awaitingCardPlayState = new AwaitingCardPlayState(_cardServices, _userInterface);

			NextState(awaitingCardPlayState);

			return awaitingCardPlayState;
		}
	}
}
