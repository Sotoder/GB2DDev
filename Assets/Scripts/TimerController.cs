using JoostenProductions;
using Tools;
using UnityEngine;


public class TimerController: BaseController
{
    private const float REQUIRED_FOR_DELETING_TIMER_TIME = 20f;

    public TimerController()
    {
        UpdateManager.SubscribeToUpdate(OnUpdate);
        AddController(this);
    }

    public void OnUpdate()
    {
        for (int i = 0; i < TimersList.Timers.Count; i++)
        {
            if ((Time.time - TimersList.Timers[i].GetStartTime) >= TimersList.Timers[i].GetDeltaTime && !TimersList.Timers[i].IsTimerEndStatus)
            {
                TimersList.Timers[i].InvokeTimerEnd();
            }

            if ((Time.time - TimersList.Timers[i].GetStartTime) >= REQUIRED_FOR_DELETING_TIMER_TIME)
            {
                TimersList.RemoveTimer(TimersList.Timers[i]);
            }
        }
    }
}
