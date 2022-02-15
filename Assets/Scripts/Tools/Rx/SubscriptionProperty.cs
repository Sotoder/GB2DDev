using System;

namespace Tools
{
    public class SubscriptionProperty<T> : IReadOnlySubscriptionProperty<T>
    {
        private T _value;
        private Action<T> _onChangeValue;
        
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onChangeValue?.Invoke(_value);
            }
        }

        public void SubscribeOnChange(Action<T> subscriptionAction)
        {
            _onChangeValue += subscriptionAction;
        }

        public void UnSubscriptionOnChange(Action<T> unsubscriptionAction)
        {
            _onChangeValue -= unsubscriptionAction;
        }
    }

    public class SubscriptionPropertyWithClassInfo<T, T2> : IReadOnlySubscriptionPropertyWithClassInfo<T, T2> where T2: class
    {
        private T _value;
        private Action<T, T2> _onChangeValue;
        private T2 _targetClass;

        public SubscriptionPropertyWithClassInfo(T2 targetClass)
        {
            _targetClass = targetClass;
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onChangeValue?.Invoke(_value, _targetClass);
            }
        }

        public void SubscribeOnChange(Action<T, T2> subscriptionAction)
        {
            _onChangeValue += subscriptionAction;
        }

        public void UnSubscriptionOnChange(Action<T, T2> unsubscriptionAction)
        {
            _onChangeValue -= unsubscriptionAction;
        }
    }
}

