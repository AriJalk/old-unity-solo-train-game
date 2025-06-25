using GameEngine.Commands;
using HexSystem;
using System;
using System.Collections.Generic;

namespace PrototypeGame.Commands
{
	internal class CommandManager
	{
		private LogicStateEvents _logicStateEvents;
		private CommandGroup _commandGroupHead;
		private Stack<CommandGroup> _commandGroupStack = new Stack<CommandGroup>();


		public CommandManager(LogicStateEvents logicStateEvents)
		{
			_logicStateEvents = logicStateEvents;
		}

		public void NextCommandGroup()
		{
			// Keep head if empty
			if (_commandGroupHead == null)
			{
				_commandGroupHead = new CommandGroup();
				return;
			}
			if (_commandGroupHead.HasCommands)
			{
				_commandGroupStack.Push(_commandGroupHead);
				_commandGroupHead = new CommandGroup();
			}
		}

		public void UndoCommandGroup()
		{
			// Reset current head and keep front
			if (_commandGroupHead?.HasCommands == true)
			{
				_commandGroupHead.UndoAll();
				return;
			}

			if (_commandGroupStack.Count > 0)
			{
				_commandGroupStack.Pop().UndoAll();
			}
		}

		public void CreateAndExecuteTrasnportCommand(Guid originSlot, Guid destinationSlot)
		{
			TransportCubeCommand command = new TransportCubeCommand(_logicStateEvents, originSlot, destinationSlot);
			command.Execute();
			_commandGroupHead.AddCommand(command);
		}

		public void CreateAndExecuteFactoryBuildCommand(HexCoord hexCoord, GoodsColor productionColor)
		{
			BuildFactoryCommand command = new BuildFactoryCommand(_logicStateEvents, hexCoord, productionColor);
			command.Execute();
			_commandGroupHead.AddCommand(command);
		}
	}
}
