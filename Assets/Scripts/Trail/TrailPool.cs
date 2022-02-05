using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailPool
{
    private List<ITrail> _pool = new List<ITrail>();
    private TrailFactory _trailFactory;
    private Transform _poolTransform;

    public TrailPool (TrailRenderer prototype, Transform poolTransform)
    {
        _trailFactory = new TrailFactory(prototype);
        _poolTransform = poolTransform;
        FillPool(5);
    }

    private void FillPool(int poolCapacity)
    {
        for (int i = 0; i < poolCapacity; i++)
        {
            var trail = _trailFactory.Create(_poolTransform);
            trail.TrailRenderer.transform.SetParent(_poolTransform);

            _pool.Add(trail);
        }
    }

    public ITrail GetFreeTrail(int fingerID)
    {
        var freeTrail = _pool.FirstOrDefault(trail => !trail.IsBusy);
        
        if(freeTrail is null)
        {
            freeTrail = _trailFactory.Create(_poolTransform);
            freeTrail.TrailRenderer.transform.SetParent(_poolTransform);

            _pool.Add(freeTrail);
        }

        freeTrail.SetBusy(fingerID);
        return freeTrail;
    }

    public ITrail GetTrailByFinger(int fingerID)
    {
        var busyTrail = _pool.FirstOrDefault(trail => trail.FingetID == fingerID && trail.IsBusy);

        return busyTrail;
    }

    public void ReturnTrail(int fingerID)
    {
        var busyTrail = _pool.FirstOrDefault(trail => trail.FingetID == fingerID);
        busyTrail.TrailRenderer.Clear();
        busyTrail.TrailRenderer.gameObject.SetActive(false);
        busyTrail.TrailRenderer.transform.position = _poolTransform.position;
        busyTrail.TrailRenderer.emitting = false;
        busyTrail.SetFree();
    }
}