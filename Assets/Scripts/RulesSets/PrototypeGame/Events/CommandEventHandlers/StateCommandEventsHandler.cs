using PrototypeGame.Events.CommandRequestEvents;
using System;
using TurnBasedHexEngine.StateMachine;

namespace PrototypeGame.Events.CommandEventHandlers
{
	internal class StateCommandEventsHandler : IDisposable
	{
		private StateMachineManager _stateMachineManager;
		StateCommandRequestEvents _stateCommandRequestEvents;

		public StateCommandEventsHandler(StateMachineManager stateMachineManager, StateCommandRequestEvents stateCommandRequestEvents)
		{
			_stateMachineManager = stateMachineManager;
			_stateCommandRequestEvents = stateCommandRequestEvents;

			_stateCommandRequestEvents.TransitionToStateMachineRequestEvent += OnTransitionToStateMachineRequest;
			
		}

		public void Dispose()
		{
			_stateCommandRequestEvents.TransitionToStateMachineRequestEvent -= OnTransitionToStateMachineRequest;
		}

		private void OnTransitionToStateMachineRequest(IStateMachine stateMachine)
		{
			_stateMachineManager.NextState(stateMachine);
		}
	}
}
