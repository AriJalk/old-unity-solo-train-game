using GameEngine.Commands;
using HexSystem;
using System;
using System.Collections.Generic;

namespace PrototypeGame.Commands
{
	internal class CommandManager
	{
		private LogicStateEvents _logicStateEvents;
		private CommandGroup _currentCommandGroup;
		private Stack<CommandGroup> _commandGroupStack = new Stack<CommandGroup>();


		public CommandManager(LogicStateEvents logicStateEvents)
		{
			_logicStateEvents = logicStateEvents;
		}

		public void StartCommandGroup()
		{
			_currentCommandGroup = new CommandGroup();
		}

		public void EndCommandGroup()
		{
			if (_currentCommandGroup.Count > 0)
			{
				_commandGroupStack.Push(_currentCommandGroup);
			}
		}

		public void UndoCommandGroup()
		{
			// Reset current head and keep front
			if (_currentCommandGroup != null && _currentCommandGroup.Count > 0)
			{
				_currentCommandGroup.UndoAll();
			}

			else if (_commandGroupStack.Count > 0)
			{
				_commandGroupStack.Pop().UndoAll();
			}
		}

		public void CreateAndExecuteTrasnportCommand(Guid originSlot, Guid destinationSlot)
		{
			TransportCubeCommand command = new TransportCubeCommand(_logicStateEvents, originSlot, destinationSlot);
			command.Execute();
			_currentCommandGroup.AddCommand(command);
		}

		public void CreateAndExecuteFactoryBuildCommand(HexCoord hexCoord, GoodsColor productionColor)
		{
			BuildFactoryCommand command = new BuildFactoryCommand(_logicStateEvents, hexCoord, productionColor);
			command.Execute();
			_currentCommandGroup.AddCommand(command);
		}
	}
}
