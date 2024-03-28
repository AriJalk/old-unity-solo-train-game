using System.Collections.Generic;

namespace SoloTrainGame.Core
{
    public class StateManager
    {
        private Queue<IGameState> _stateQueue;
        private IGameState _currentState;

        public StateManager() 
        {
            _stateQueue = new Queue<IGameState>();
        }

        public void AddState(IGameState state)
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