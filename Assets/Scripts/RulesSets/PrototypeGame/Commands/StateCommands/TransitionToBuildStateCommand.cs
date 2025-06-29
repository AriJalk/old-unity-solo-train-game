using GameEngine.Commands;
using GameEngine.StateMachine;
using PrototypeGame.Events;
using PrototypeGame.StateMachine;
using System;

namespace Commands.StateCommands
{
	internal class TransitionToBuildStateCommand : ICommand
	{
		private int _availableMoney;

		private CommandRequestEvents _commandRequestEvents;
		private StateMachineFactory _stateMachineFactory;

		public TransitionToBuildStateCommand(int availableMoney, CommandRequestEvents commandRequestEvents, StateMachineFactory stateMachineFactory) 
		{ 
			_availableMoney = availableMoney;
			_commandRequestEvents = commandRequestEvents;
			_stateMachineFactory = stateMachineFactory;
		}


		public void Execute()
		{
			IStateMachine state = _stateMachineFactory.CreateBuildActionState(_availableMoney);
			_commandRequestEvents.RaiseTransitionToStateMachineEvent(state);
		}


		public void Undo()
		{
			_commandRequestEvents.RaiseTransitionToPreviousMachineEvent();
		}
	}
}
