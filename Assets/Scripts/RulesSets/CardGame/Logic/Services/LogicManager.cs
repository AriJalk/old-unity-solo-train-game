using UnityEngine;

namespace CardGame.Logic.Services
{
	internal class LogicManager
	{
		private LogicGameState _logicGameState;
		public LogicManager(LogicGameState logicGameState)
		{
			_logicGameState = logicGameState;
		}
	}
}
