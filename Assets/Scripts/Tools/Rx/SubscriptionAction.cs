using System;

namespace Tools
{
    public class SubscriptionAction : IReadOnlySubscriptionAction
    {
        private Action _action;

        public void Invoke()
        {
            _action?.Invoke();
        }

        public void SubscribeOnChange(Action subscriptionAction)
        {
            _action += subscriptionAction;
        }

        public void UnSubscriptionOnChange(Action unsubscriptionAction)
        {
            _action -= unsubscriptionAction;
        }
    }

    public class SubscriptionAction<T> : IReadOnlySubscriptionAction<T>
    {
        private Action<T> _action;

        public void Invoke(T parameter)
        {
            _action?.Invoke(parameter);
        }

        public void SubscribeOnChange(Action<T> subscriptionAction)
        {
            _action += subscriptionAction;
        }

        public void UnSubscriptionOnChange(Action<T> unsubscriptionAction)
        {
            _action -= unsubscriptionAction;
        }
    }
}