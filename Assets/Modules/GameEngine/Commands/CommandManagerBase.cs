using System.Collections.Generic;

namespace GameEngine.Commands
{
	public class CommandManagerBase
	{
		protected CommandGroup _commandGroupHead;
		protected Stack<CommandGroup> _commandGroupStack = new Stack<CommandGroup>();

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
	}
}
