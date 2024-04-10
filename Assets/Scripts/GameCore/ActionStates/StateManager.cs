using Engine;
using System.Collections.Generic;

namespace SoloTrainGame.Core
{
    public class StateManager
    {
        // undo
        private Queue<IActionState> _stateQueue;
        private Stack<IActionState> _undoStack;

        public IActionState CurrentState { get; private set; }

        public StateManager()
        {
            _stateQueue = new Queue<IActionState>();
            _undoStack = new Stack<IActionState>();
        }

        public void AddState(IActionState state)
        {
            _stateQueue.Enqueue(state);
        }

        public void EnterNextState()
        {
            if (CurrentState == null && _stateQueue.Count > 0)
            {
                CurrentState = _stateQueue.Dequeue();
                CurrentState.OnEnterGameState();
            }
        }

        public void ExitCurrentState()
        {
            if (CurrentState != null)
            {
                CurrentState.OnExitGameState();
                CurrentState = null;
                _undoStack.Push(CurrentState);
                if (_stateQueue.Count > 0)
                {
                    EnterNextState();
                }
            }

        }
    }
}