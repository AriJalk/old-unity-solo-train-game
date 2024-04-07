using System.Collections.Generic;

namespace SoloTrainGame.Core
{
    /// <summary>
    /// Represents a sequence of commands
    /// </summary>
    public class Turn
    {
        private Stack<IGameCommand> _commandStack;

        public Turn()
        {
            _commandStack = new Stack<IGameCommand>();
        }

        public bool AddCommandAndExecute(IGameCommand command)
        {
            if (command.CanExecute)
            {
                _commandStack.Push(command);
                return true;
            }
            return false;
        }

        public void UndoAllCommands()
        {
            while(_commandStack.Count > 0)
            {
                UndoLastCommand();
            }
        }

        public void UndoLastCommand()
        {
            IGameCommand command = _commandStack.Pop();
            command.Undo();
        }
    }
}