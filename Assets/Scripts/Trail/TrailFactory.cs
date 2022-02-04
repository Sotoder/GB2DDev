using UnityEngine;

public class TrailFactory
{
    private TrailRenderer _prototype;

    public TrailFactory(TrailRenderer prototype)
    {
        _prototype = prototype;
    }
    public TrailRenderer Create(Transform transform)
    {
        var trail = GameObject.Instantiate(_prototype, transform);
        trail.gameObject.SetActive(false);

        return trail;
    }
}