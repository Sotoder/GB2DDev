using System;
using System.Collections.Generic;
using Profile;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainMenuController : BaseController
{
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private readonly ShedController _shedController;
    private AsyncOperationHandle<GameObject> _mainMenuViewHandle;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, List<ItemConfig> itemsConfig, 
                              IReadOnlyList<UpgradeItemConfig> upgradeItems, InventoryModel inventoryModel, IMainMenuReferences prefabsAssetReferences)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(prefabsAssetReferences.MainMenuReference, placeForUi);
        _shedController = new ShedController(upgradeItems, itemsConfig, _profilePlayer.CurrentCar, _view.transform, inventoryModel);
        AddGameObjects(_view.gameObject);
        _view.Init(StartGame, _shedController.Enter, OpenRewardWindow);
    }

    private void OpenRewardWindow()
    {
        _profilePlayer.CurrentState.Value = GameState.Rewards;
    }

    private MainMenuView LoadView(AssetReferenceGameObject mainMenuRef, Transform placeForUi)
    {
        var result = ResourceLoader.AdressableLoadAndInstantiateView<MainMenuView>(mainMenuRef, placeForUi);
        _mainMenuViewHandle = result.handle;
        return result.view;
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;

        _profilePlayer.AnalyticTools.SendMessage("start_game",
            new Dictionary<string, object>() { {"time", Time.realtimeSinceStartup }
            });
    }

    protected override void OnDispose()
    {
        if (_mainMenuViewHandle.IsValid())
        {
            Addressables.Release(_mainMenuViewHandle);
        }
    }
}

