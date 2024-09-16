using UnityEngine;

public class Trail: ITrail
{
    private TrailRenderer _trailRenderer;
    private int _fingetID;
    private bool _isBusy;

    public TrailRenderer TrailRenderer { get => _trailRenderer; }
    public int FingetID { get => _fingetID; }
    public bool IsBusy { get => _isBusy; }

    public Trail(TrailRenderer trailRenderer)
    {
        _trailRenderer = trailRenderer;
    }

    public void SetBusy (int fingerID)
    {
        _fingetID = fingerID;
        _isBusy = true;
    }

    public void SetFree()
    {
        _fingetID = 0;
        _isBusy = false;
    }
}