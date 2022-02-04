using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailPool
{
    private List<TrailRenderer> _pool = new List<TrailRenderer>();
    private Dictionary<int, TrailRenderer> _busyTrailsByFingers = new Dictionary<int, TrailRenderer>();
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
            trail.transform.SetParent(_poolTransform);

            _pool.Add(trail);
        }
    }

    public TrailRenderer GetFreeTrail(int fingerID)
    {
        var freeTrail = _pool.FirstOrDefault(trail => !_busyTrailsByFingers.Values.Contains(trail));
        
        if(freeTrail is null)
        {
            freeTrail = _trailFactory.Create(_poolTransform);
            freeTrail.transform.SetParent(_poolTransform);

            _pool.Add(freeTrail);
        }

        _busyTrailsByFingers[fingerID] = freeTrail;

        return freeTrail;
    }

    public TrailRenderer GetTrailByFinger(int fingerID)
    {
        if(_busyTrailsByFingers.TryGetValue(fingerID, out var trail))
        {
            return trail;
        }

        return null;
    }

    public void ReturnTrail(int fingerID)
    {
        var trail = _busyTrailsByFingers[fingerID];
        trail.Clear();
        trail.gameObject.SetActive(false);
        trail.transform.position = _poolTransform.position;
        trail.emitting = false;
        _busyTrailsByFingers.Remove(fingerID);
    }
}