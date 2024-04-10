using System.Collections.Generic;

namespace SoloTrainGame.Core
{
    public class StateManager
    {
        private Queue<IActionState> _stateQueue;
        
        public IActionState CurrentState { get; private set; }

        public StateManager() 
        {
            _stateQueue = new Queue<IActionState>();
        }

        public void AddState(IActionState state)
        {
            _stateQueue.Enqueue(state);
        }

        public void RemoveLastState()
        {
            _stateQueue.Dequeue();
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
            }
        }
    }
}