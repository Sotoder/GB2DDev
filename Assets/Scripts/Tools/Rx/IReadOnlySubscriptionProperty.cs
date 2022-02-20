using System;

namespace Tools
{
    public interface IReadOnlySubscriptionProperty<T>
    { 
        T Value { get; }
        void SubscribeOnChange(Action<T> subscriptionAction);
        void UnSubscriptionOnChange(Action<T> unsubscriptionAction);
    }

    public interface IReadOnlySubscriptionPropertyWithClassInfo<T, T2> where T2: class
    {
        T Value { get; }
        void SubscribeOnChange(Action<T, T2> subscriptionAction);
        void UnSubscriptionOnChange(Action<T, T2> unsubscriptionAction);
    }
}

