using UnityEngine;
using UnityEngine.Events;

namespace Engine
{
    public class OnEndTimer : TimerBase
    {

        public UnityEvent TimerFinishedEvent;

        public override void StartTimer(float time)
        {
            base.StartTimer(time);
            TimerFinishedEvent = new UnityEvent();
        }
        public override void StopTimer()
        {
            TimerFinishedEvent?.Invoke();
            base.StopTimer();
        }

    }

}