using Tools;
using UnityEngine;

public abstract class BaseInputView : MonoBehaviour
{
    private SubscriptionProperty<float> _leftMove;
    private SubscriptionProperty<float> _rightMove;
    private SubscriptionProperty<bool> _isStay;

    protected float _speed;
    
    public virtual void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, SubscriptionProperty<bool> isStay, float speed)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        _isStay = isStay;
        _speed = speed;
    }
    
    protected void OnLeftMove(float value)
    {
        _leftMove.Value = value;

        if (_isStay.Value)
            _isStay.Value = false;
    }

    protected void OnRightMove(float value)
    {
        _rightMove.Value = value;

        if (_isStay.Value)
            _isStay.Value = false;
    }

    protected void OnStay()
    {
        if(!_isStay.Value)
            _isStay.Value = true;
    }
}

