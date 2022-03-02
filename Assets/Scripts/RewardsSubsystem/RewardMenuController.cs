using Profile;
using System;
using UnityEngine;

public class RewardMenuController: BaseController
{
    private ProfilePlayer _profilePlayer;
    private Transform _placeForUI;
    private RewardsWindow _rewardsWindow;

    public RewardMenuController(ProfilePlayer profilePlayer, Transform placeForUi)
    {
        _profilePlayer = profilePlayer;
        _placeForUI = placeForUi;

        ConfigureateAndShowRewardWindow();
        AddController(this);
    }

    private void ConfigureateAndShowRewardWindow()
    {
        _rewardsWindow = ResourceLoader.LoadAndInstantiateView<RewardsWindow>(
            new ResourcePath { PathResource = "Prefabs/Rewards/RewardsWindow" }, _placeForUI);
        _rewardsWindow
            .ConfigurateRewardSystem(_profilePlayer)
            .Show();
        AddGameObjects(_rewardsWindow.gameObject);
        _rewardsWindow.UserCloseRewardsWindow += CloseRewardsWindow;
    }

    private void CloseRewardsWindow()
    {
        _profilePlayer.CurrentState.Value = GameState.Start;
    }
}