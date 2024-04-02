using System;
using UnityEngine;

namespace Engine
{
    /// <summary>
    /// Performs an action every update
    /// </summary>
    public class OnUpdateTimer : TimerBase
    {
        private Action _onUpdateAction;

        public void StartTimer(float duration, Action onUpdateAction)
        {
            base.StartTimer(duration);
            _onUpdateAction = onUpdateAction;
        }

        public override void ExecuteStep()
        {
            _onUpdateAction?.Invoke();
        }

        public override void StopTimer()
        {
            base.StopTimer();
            _onUpdateAction = null;
        }
    }
}
