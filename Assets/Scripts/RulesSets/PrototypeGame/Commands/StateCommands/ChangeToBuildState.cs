using GameEngine.StateMachine;
using System;

namespace Commands.StateCommands
{
	internal class ChangeToBuildState : IStateMachine
	{
		private int _availableMoney;

		

		public ChangeToBuildState(int availableMoney) 
		{ 
			_availableMoney = availableMoney;
		}
		public void EnterState()
		{
			throw new NotImplementedException();
		}

		public void ExitState()
		{
			throw new NotImplementedException();
		}
	}
}
