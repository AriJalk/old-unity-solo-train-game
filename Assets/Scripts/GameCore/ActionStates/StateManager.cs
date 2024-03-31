using System.Collections.Generic;

namespace SoloTrainGame.Core
{
    public class StateManager
    {
        private Queue<IActionState> _stateQueue;
        private IActionState _currentState;

        public StateManager() 
        {
            _stateQueue = new Queue<IActionState>();
        }

        public void AddState(IActionState state)
        {
            _stateQueue.Enqueue(state);
        }

        public void EnterNextState()
        {
            if (_currentState == null && _stateQueue.Count > 0)
            {
                _currentState = _stateQueue.Dequeue();
                _currentState.OnEnterGameState();
            }
        }

        public void ExitCurrentState() 
        { 
            if (_currentState != null)
            {
                _currentState.OnExitGameState();
                _currentState = null;
            }
        }
    }
}