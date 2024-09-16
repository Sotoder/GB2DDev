﻿using JoostenProductions;
using Tools;
using UnityEngine;

public class InputGyroView : BaseInputView
{
    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        Input.gyro.enabled = true;
        UpdateManager.SubscribeToUpdate(Move);
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
    }

    private void Move()
    {
        if (!SystemInfo.supportsGyroscope)
            return;
        var quaternion = Input.gyro.attitude;
        quaternion.Normalize();
        OnRightMove((quaternion.x + quaternion.y) * Time.deltaTime * _speed);
    }
}
