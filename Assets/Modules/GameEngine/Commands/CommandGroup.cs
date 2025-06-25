using System.Collections.Generic;

namespace GameEngine.Commands
{
	/// <summary>
	/// Atomic group of commands
	/// </summary>
	public class CommandGroup
	{
		private Stack<ICommand> _commandStack = new Stack<ICommand>();

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
