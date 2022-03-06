using System;
using Tools;
public class RewardData
{
    public readonly int RewardViewID;
    public SubscriptionProperty<int> CurrentActiveSlot;
    public SubscriptionProperty<DateTime?> LastRewardTime;

    public RewardData(int viewID)
    {
        CurrentActiveSlot = new SubscriptionProperty<int>();
        LastRewardTime = new SubscriptionProperty<DateTime?>();
        RewardViewID = viewID;
    }
}