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
		public IStateMachine CurrentState {  get; private set; }

		public StateMachineManager()
		{
			
		}

		public void NextState(IStateMachine nextState)
		{
			if (CurrentState != null)
			{
				CurrentState.ExitState();
			}
			nextState.EnterState();
			CurrentState = nextState;
		}

	}
}
