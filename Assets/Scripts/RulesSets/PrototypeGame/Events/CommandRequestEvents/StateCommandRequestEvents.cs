using GameEngine.StateMachine;
using System;


namespace PrototypeGame.Events.CommandRequestEvents
{
	internal class StateCommandRequestEvents
	{

		public event Action<IStateMachine> TransitionToStateMachineRequestEvent;

		public void RaiseTransitionToStateMachineEvent(IStateMachine stateMachine)
		{
			TransitionToStateMachineRequestEvent?.Invoke(stateMachine);
		}

	}
}
