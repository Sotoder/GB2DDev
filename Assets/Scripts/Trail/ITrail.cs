using UnityEngine;

public interface ITrail
{
    TrailRenderer TrailRenderer { get; }
    int FingetID { get; }
    bool IsBusy { get; }
    void SetBusy(int fingerID);
    void SetFree();
}