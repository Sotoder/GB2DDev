using UnityEngine;

public class TrailFactory
{
    private TrailRenderer _prototype;

    public TrailFactory(TrailRenderer prototype)
    {
        _prototype = prototype;
    }
    public ITrail Create(Transform transform)
    {
        var trailRenderer = GameObject.Instantiate(_prototype, transform);
        trailRenderer.gameObject.SetActive(false);

        var trail = new Trail(trailRenderer);

        return trail;
    }
}