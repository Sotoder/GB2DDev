using System;
using UnityEngine;

namespace Tools
{
    public class Timer
    {

        public event Action<Timer> TimerIsOver = delegate (Timer timer) { };

        private bool IsTimerEnd;

        private readonly float _startTime;
        private readonly float _deltaTime;

        public bool IsTimerEndStatus { get => IsTimerEnd; }
        public float GetStartTime { get => _startTime; }
        public float GetDeltaTime { get => _deltaTime; }

        public Timer(float deltaTime)
        {
            TimerIsOver += ChangeTimerStatus;
            IsTimerEnd = false;
            _startTime = Time.time;
            _deltaTime = deltaTime;
        }

        private void ChangeTimerStatus(Timer timer)
        {
            IsTimerEnd = true;
        }

        public void InvokeTimerEnd()
        {
            TimerIsOver.Invoke(this);
        }

        public void Dispose()
        {
            TimerIsOver -= ChangeTimerStatus;
        }
    }
}