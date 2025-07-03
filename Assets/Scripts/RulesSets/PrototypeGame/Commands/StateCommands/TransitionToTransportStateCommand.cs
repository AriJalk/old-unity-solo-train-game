using PrototypeGame.Events.CommandRequestEvents;
using PrototypeGame.StateMachine;
using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.StateMachine;

namespace PrototypeGame.Commands.StateCommands
{
	internal class TransitionToTransportStateCommand : ICommand
	{
		private StateCommandRequestEvents _stateCommandRequestEvents;
		private StateMachineFactory _stateMachineFactory;
		private IStateMachine _previousState;

		private int _transportPoints;

		public TransitionToTransportStateCommand(StateCommandRequestEvents stateCommandRequestEvents, StateMachineFactory stateMachineFactory, int transportPoints, IStateMachine previousState)
		{
			_stateCommandRequestEvents = stateCommandRequestEvents;
			_stateMachineFactory = stateMachineFactory;
			_previousState = previousState;
			_transportPoints = transportPoints;
		}
		public void Execute()
		{
			IStateMachine state = _stateMachineFactory.CreateTransportActionState(_transportPoints);

			_stateCommandRequestEvents.RaiseTransitionToStateMachineEvent(state);
		}

		public void Undo()
		{
			_stateCommandRequestEvents.RaiseTransitionToStateMachineEvent(_previousState);
		}
	}
}
