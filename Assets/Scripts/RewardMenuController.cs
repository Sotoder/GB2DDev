using System;
using UnityEngine;

internal class RewardMenuController: IDisposable
{
    private ProfilePlayer _profilePlayer;
    private Transform _placeForUI;
    private RewardsWindow _rewardsWindow;

    public RewardMenuController(ProfilePlayer profilePlayer, Transform placeForUi)
    {
        _profilePlayer = profilePlayer;
        _placeForUI = placeForUi;

        ConfigureateAndShowRewardWindow();
    }

    private void ConfigureateAndShowRewardWindow()
    {
        _rewardsWindow = ResourceLoader.LoadAndInstantiateView<RewardsWindow>(
            new ResourcePath { PathResource = "Prefabs/Rewards/RewardsWindow" }, _placeForUI);
        _rewardsWindow
            .ConfigurateRewardSystem(_profilePlayer)
            .Show();
    }

    public void Dispose()
    {
        _rewardsWindow?.Hide();
    }
}