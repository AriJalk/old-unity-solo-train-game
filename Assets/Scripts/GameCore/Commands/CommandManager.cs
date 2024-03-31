using System.Collections.Generic;

namespace SoloTrainGame.Core
{
    public class CommandManager
    {
        private Stack<IGameCommand> _commandStack;

        public CommandManager()
        {
            _commandStack = new Stack<IGameCommand>();
        }

        public bool AddAndExecuteCommand(IGameCommand command)
        {
            if (command.CanExecute)
            {
                _commandStack.Push(command);
                return true;
            }
            return false;
        }

        public void Undo()
        {
            IGameCommand command = _commandStack.Pop();
            command.Undo();
        }
    }
}