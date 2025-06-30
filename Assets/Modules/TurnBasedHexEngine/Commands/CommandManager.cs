using System.Collections.Generic;

namespace TurnBasedHexEngine.Commands
{
	/// <summary>
	/// Manages command flow
	/// </summary>
	public class CommandManager
	{
		private CommandGroup _commandGroupHead;
		private Stack<CommandGroup> _commandGroupStack = new Stack<CommandGroup>();

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
			// Undo last group
			if (_commandGroupStack.Count > 0)
			{
				_commandGroupStack.Pop().UndoAll();
			}
		}

		public void PushAndExecuteCommand(ICommand command)
		{
			_commandGroupHead.AddCommand(command);
			command.Execute();
		}
	}
}
