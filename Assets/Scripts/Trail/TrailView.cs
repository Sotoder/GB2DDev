﻿using JoostenProductions;
using System;
using UnityEngine;

public class TrailView : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    private TrailPool _trailPool;


    public void Init()
    {
        _trailPool = new TrailPool(_trailRenderer, this.transform);
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        foreach (var touch in Input.touches)
        {
            CheckTouch(touch);
        }
    }

    private void CheckTouch(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                StartTouch(touch);
                break;
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                TouchInProgress(touch);
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                ClearTouch(touch);
                break;
        }
    }

    private void StartTouch(Touch touch)
    {
        var trail = _trailPool.GetFreeTrail(touch.fingerId);
        trail.transform.position = GetWorldPosition(touch);
        trail.emitting = true;
        trail.gameObject.SetActive(true);
    }

    private void TouchInProgress(Touch touch)
    {
        var trail = _trailPool.GetTrailByFinger(touch.fingerId);
        
        if(trail is null)
        {
            trail = _trailPool.GetFreeTrail(touch.fingerId);
        }

        trail.transform.position = GetWorldPosition(touch);
        trail.emitting = true;
        trail.gameObject.SetActive(true);
    }

    private void ClearTouch(Touch touch)
    {
        _trailPool.ReturnTrail(touch.fingerId);
    }

    private Vector3 GetWorldPosition(Touch touch)
    {
        var position = Camera.main.ScreenToWorldPoint(touch.position);
        position.z = this.transform.position.z;

        return position;
    }


}
