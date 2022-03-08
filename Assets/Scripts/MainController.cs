using Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, AppDataSourses appDataSourses, PrefabsAssetReferences prefabsAssetReferences)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;

        _appDataSourses = appDataSourses;
        _prefabsAssetReferences = prefabsAssetReferences;

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);

        LoadItemsConfigs();

        _inventoryModel = new InventoryModel();
    }

    private void LoadItemsConfigs()
    {
        _upgradeItemOperationHandle = ResourceLoader.LoadDataSource<UpgradeItemsSource>(_appDataSourses.LoadUpgradeSource);
        _upgradeSource = _upgradeItemOperationHandle.WaitForCompletion();

        _itemOperationHandle = ResourceLoader.LoadDataSource<ItemsSource>(_appDataSourses.LoadItemSource);
        _itemsSource = _itemOperationHandle.WaitForCompletion();

        _abilityItemOperationHandle = ResourceLoader.LoadDataSource<AbilitiesItemsSource>(_appDataSourses.LoadAbilitySource);
        _abilityItemsSource = _abilityItemOperationHandle.WaitForCompletion();

    }

    private AppDataSourses _appDataSourses;
    private AsyncOperationHandle<UpgradeItemsSource> _upgradeItemOperationHandle;
    private AsyncOperationHandle<ItemsSource> _itemOperationHandle;
    private AsyncOperationHandle<AbilitiesItemsSource> _abilityItemOperationHandle;
    private UpgradeItemsSource _upgradeSource;
    private ItemsSource _itemsSource;
    private AbilitiesItemsSource _abilityItemsSource;

    private PrefabsAssetReferences _prefabsAssetReferences;
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private InventoryModel _inventoryModel;
    private FightController _fightController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private RewardMenuController _rewardMenuController;

    private List<ItemConfig> _itemConfigs => _itemsSource.Content.ToList();
    private List<UpgradeItemConfig> _upgradeItemConfigs => _upgradeSource.Content.ToList();
    private List<AbilityItemConfig> _abilityItemConfigs => _abilityItemsSource.Content.ToList();

    protected override void OnDispose()
    {
        AllClear();

        Addressables.ReleaseInstance(_upgradeItemOperationHandle);
        Addressables.ReleaseInstance(_itemOperationHandle);
        Addressables.ReleaseInstance(_abilityItemOperationHandle);

    _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _itemConfigs, _upgradeItemConfigs, _inventoryModel, _prefabsAssetReferences);
                _gameController?.Dispose();
                _rewardMenuController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer, _abilityItemConfigs, _itemConfigs, _upgradeItemConfigs, _inventoryModel, _placeForUi, _prefabsAssetReferences);
                _mainMenuController?.Dispose();
                _fightController?.Dispose();
                break;
            case GameState.Rewards:
                _rewardMenuController = new RewardMenuController(_profilePlayer, _placeForUi);
                _mainMenuController?.Dispose();
                break;
            case GameState.Fight:
                _gameController?.Dispose();
                _fightController = new FightController(_profilePlayer, _placeForUi);
                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _rewardMenuController?.Dispose();
        _fightController?.Dispose();
    }
}
