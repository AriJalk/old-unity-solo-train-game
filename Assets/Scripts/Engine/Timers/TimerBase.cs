using UnityEngine;

namespace Engine
{
    public abstract class TimerBase
    {
        protected bool _isRunning;
        protected float _timeLeft;


        public void Update()
        {
            if (_isRunning && _timeLeft > 0)
            {
                ExecuteStep();
                _timeLeft -= Time.deltaTime * Time.timeScale;
                if (_timeLeft <= 0)
                {
                    StopTimer();
                }
            }
        }

        public virtual void StartTimer(float duration)
        {
            if (duration > 0 && !_isRunning)
            {
                _timeLeft = duration;
                _isRunning = true;
            }
        }
        public virtual void StopTimer()
        {
            _isRunning = false;
            _timeLeft = 0;
            ServiceLocator.TimerManager.ReturnTimer(this);
        }

        public virtual void ExecuteStep() { }

        public override string ToString()
        {
            return "TimeLeft: " + _timeLeft;
        }

    }
}