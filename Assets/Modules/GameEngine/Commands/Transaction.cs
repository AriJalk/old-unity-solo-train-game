using System.Collections.Generic;

namespace GameEngine.Commands
{
	/// <summary>
	/// Represents a single atomic sequence of commands
	/// </summary>
	public class Transcation
	{
		private Stack<ICommand> _commandStack = new Stack<ICommand>();

		public void PushCommand(ICommand command)
		{
			_commandStack.Push(command);
		}

		public void UndoAllCommands()
		{
			while (_commandStack.Count > 0)
			{
				ICommand command = _commandStack.Pop();
				command.Undo();
			}
		}
	}

}
