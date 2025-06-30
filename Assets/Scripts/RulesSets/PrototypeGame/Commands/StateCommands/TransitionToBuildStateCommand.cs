using TurnBasedHexEngine.Commands;
using TurnBasedHexEngine.StateMachine;
using PrototypeGame.Events;
using PrototypeGame.Events.CommandRequestEvents;
using PrototypeGame.StateMachine;
using System;

namespace Commands.StateCommands
{
	internal class TransitionToBuildStateCommand : ICommand
	{
		private int _availableMoney;

		private StateCommandRequestEvents _stateCommandRequestEvents;
		private StateMachineFactory _stateMachineFactory;
		private IStateMachine _previousState;

		public TransitionToBuildStateCommand(StateCommandRequestEvents stateCommandRequestEvents, StateMachineFactory stateMachineFactory, int availableMoney, IStateMachine previousState) 
		{ 
			_stateCommandRequestEvents = stateCommandRequestEvents;
			_stateMachineFactory = stateMachineFactory;
			_availableMoney = availableMoney;
			_previousState = previousState;
		}


			public void Execute()
			{
				IStateMachine state = _stateMachineFactory.CreateBuildActionState(_availableMoney);
				_stateCommandRequestEvents.RaiseTransitionToStateMachineEvent(state);
			}

			public void Undo()
			{
				_stateCommandRequestEvents.RaiseTransitionToStateMachineEvent(_previousState);
			}
	}
}
