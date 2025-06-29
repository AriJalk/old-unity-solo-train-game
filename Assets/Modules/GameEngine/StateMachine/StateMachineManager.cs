using CardSystem;
using PrototypeGame.Commands;
using PrototypeGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.StateMachine
{
	internal class StateMachineManager
	{
		//TODO: maybe get rid of this? hold only 1?
		private Stack<IStateMachine> _states;

		public StateMachineManager()
		{
			_states = new Stack<IStateMachine>();
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
	}
}
