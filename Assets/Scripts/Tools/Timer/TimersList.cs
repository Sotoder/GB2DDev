using System.Collections.Generic;

namespace Tools
{
    public static class TimersList
    {
        private static List<Timer> _timers = new List<Timer>();

        public static List<Timer> Timers { get => _timers; }

        public static void AddTimer(Timer timer)
        {
            _timers.Add(timer);
        }

        public static void RemoveTimer(Timer timer)
        {
            _timers.Remove(timer);
        }
    }
}