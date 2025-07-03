using PrototypeGame.Events.CommandRequestEvents;
using PrototypeGame.StateMachine;
using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.StateMachine;

namespace Commands.StateCommands
{
	internal class TransitionToAwatingPlayCardForActionCommand : ICommand
	{
		private StateCommandRequestEvents _stateCommandRequestEvents;
		private StateMachineFactory _stateMachineFactory;
		private IStateMachine _previousState;

		public TransitionToAwatingPlayCardForActionCommand(StateCommandRequestEvents stateCommandRequestEvents, StateMachineFactory stateMachineFactory, IStateMachine previousState) 
		{ 
			_stateCommandRequestEvents = stateCommandRequestEvents;
			_stateMachineFactory = stateMachineFactory;
			_previousState = previousState;
		}


			public void Execute()
			{
				IStateMachine state = _stateMachineFactory.CreateAwatingPlayCardForActionState();
				_stateCommandRequestEvents.RaiseTransitionToStateMachineEvent(state);
			}

			public void Undo()
			{
				_stateCommandRequestEvents.RaiseTransitionToStateMachineEvent(_previousState);
			}
	}
}
