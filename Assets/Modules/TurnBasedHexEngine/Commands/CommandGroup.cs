using System.Collections.Generic;

namespace TurnBasedHexEngine.Commands
{
	/// <summary>
	/// Atomic group of commands
	/// </summary>
	internal class CommandGroup
	{
		private Stack<ICommand> _commandStack = new Stack<ICommand>();

		public bool HasCommands
		{
			get
			{
				return _commandStack.Count > 0;
			}
		}

		public void AddCommand(ICommand command)
		{
			_commandStack.Push(command);
		}

		public void UndoAll()
		{
			while (_commandStack.Count > 0)
			{
				ICommand command = _commandStack.Pop();
				command.Undo();
			}
		}
	}
}
