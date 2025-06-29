using PrototypeGame.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
