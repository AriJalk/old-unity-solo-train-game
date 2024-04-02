using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class TimerManager
    {
        //TODO: unify timer types to 1 collection
        private List<OnEndTimer> _activeOnEndTimers;
        private Queue<OnEndTimer> _finishedOnEndTimers;

        private List<TimerBase> _activeTimers;
        private List<TimerBase> _finishedTimers;

        private List<OnUpdateTimer> _activeOnUpdateTimers;
        private Queue<OnUpdateTimer> _finishedOnUpdateTimers;

        public TimerManager()
        {
            _activeOnEndTimers = new List<OnEndTimer>();
            _finishedOnEndTimers = new Queue<OnEndTimer>();
            _activeOnUpdateTimers = new List<OnUpdateTimer>();
            _finishedOnUpdateTimers = new Queue<OnUpdateTimer>();
            //TestTimerManager();
        }

        public void Update()
        {
            // Create a copy of the list
            List<OnEndTimer> onEndTimersCopy = new List<OnEndTimer>(_activeOnEndTimers);
            List<OnUpdateTimer> onUpdateTimersCopy = new List<OnUpdateTimer>(_activeOnUpdateTimers);
            // Iterate over the copy
            foreach (OnEndTimer timer in onEndTimersCopy)
            {
                timer.Update();
            }
            foreach(OnUpdateTimer timer in onUpdateTimersCopy)
            {
                timer.Update();
            }

        }

        public OnEndTimer GetOnEndTimer(float duration)
        {
            OnEndTimer timer = null;
            if (duration > 0)
            {
                if (_finishedOnEndTimers.Count > 0)
                {
                    timer = _finishedOnEndTimers.Dequeue();
                }
                else
                {
                    timer = new OnEndTimer();
                    timer.StartTimer(duration);
                    _activeOnEndTimers.Add(timer);
                }
            }
            return timer;
        }

        public OnUpdateTimer GetOnUpdateTimer(float duration, Action action)
        {
            OnUpdateTimer timer = null;
            if (duration > 0)
            {
                if (_finishedOnUpdateTimers.Count > 0)
                {
                    timer = _finishedOnUpdateTimers.Dequeue();
                }
                else
                {
                    timer = new OnUpdateTimer();
                    timer.StartTimer(duration, action);
                    _activeOnUpdateTimers.Add(timer);
                }
            }
            return timer;
        }


        public void ReturnTimer<T> (T timer) where T : TimerBase
        {
            if (timer is OnEndTimer endTimer)
            {
                _finishedOnEndTimers.Enqueue(endTimer);
                _activeOnEndTimers.Remove(endTimer);
            }
            else if (timer is OnUpdateTimer updateTimer)
            {
                _finishedOnUpdateTimers.Enqueue(updateTimer);
                _activeOnUpdateTimers.Remove(updateTimer);
            }
        }

        public void TestTimerManager()
        {
            TimerBase a = GetOnUpdateTimer(1, TestAction);
            TimerBase b = GetOnEndTimer(2);
            TimerBase c = GetOnEndTimer(3);
            TimerBase d = GetOnUpdateTimer(4, TestAction);
        }

        private void TestAction()
        {
            Debug.Log(Time.deltaTime);
        }
    }
}