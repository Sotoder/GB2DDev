using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class RewardsWindow : MonoBehaviour, IView
{
    [SerializeField]
    private List<RewardView> _rewardViews;
    [SerializeField]
    private RewardsContainerSwitcher _rewardsContainerSwitcher;
    [SerializeField]
    private CurrencyWindow _currencyWindow;
    [SerializeField]
    private CustomButton _exitButton;

    private RewardController _controller;
    private MementoSaver _mementoSaver;
    private SaveLoadDataController _saveDataController;

    public Action UserCloseRewardsWindow;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private void ConfigurateCurrencyWindow(CurrencyData currency)
    {
        _currencyWindow.SetProfile(currency);
        _exitButton.onClick.AddListener(ExitButtonClick);
    }

    private void ExitButtonClick()
    {
        UserCloseRewardsWindow?.Invoke();
    }

    public RewardsWindow ConfigurateRewardSystem(ProfilePlayer profilePlayer)
    {
        ConfigurateCurrencyWindow(profilePlayer.Currency);

        foreach (var view in _rewardViews)
        {
            var rewardData = new RewardData(view.ID);
            profilePlayer.RewardsData.Add(rewardData);
            view.ConfigurateRewardView(rewardData);
        }

        return this;
    }

    public void Show()
    {
        _mementoSaver = new MementoSaver(new List<ISavebleRewardView>(_rewardViews), _currencyWindow);
        var loadManager = new LoadManager(new List<ILoadableRewardView>(_rewardViews), _currencyWindow);
        _saveDataController = new SaveLoadDataController(_mementoSaver, new List<IViewWithSaveAndLoadButton>(_rewardViews), loadManager);
        _saveDataController.Load();

        _controller = new RewardController(_rewardViews, _currencyWindow);

        _rewardsContainerSwitcher.Init(new List<ISwitchableRewardView>(_rewardViews), _controller);

        _disposables.Add(_mementoSaver);
        _disposables.Add(_controller);
        _disposables.Add(_saveDataController);
    }

    public void Hide()
    {
        GameObject.Destroy(this);
    }
    private void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
        foreach (var element in _disposables)
        {
            element.Dispose();
        };
    }
}
