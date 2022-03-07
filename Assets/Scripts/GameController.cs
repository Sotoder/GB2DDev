using System;
using System.Collections.Generic;
using Features.AbilitiesFeature;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class GameController : BaseController
{
    private Button _inventoryButton;
    private InventoryController _inventoryController;
    private List<AsyncOperationHandle<GameObject>> _viewsOperationHandle = new List<AsyncOperationHandle<GameObject>>(); 
    public GameController(ProfilePlayer profilePlayer, IReadOnlyList<AbilityItemConfig> configs,
                          List<ItemConfig> itemsConfig, IReadOnlyList<UpgradeItemConfig> upgradeItems, InventoryModel inventoryModel, Transform uiRoot, 
                          IGameReferences prefabsAssetReferences)
    {
        _inventoryController = new InventoryController(itemsConfig, upgradeItems, uiRoot, inventoryModel);
        _inventoryController.SetOnGameSceneFlag(true);
        AddController(_inventoryController);

        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        var isStay = new SubscriptionProperty<bool>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar, prefabsAssetReferences.PathViewReference);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, isStay, profilePlayer.CurrentCar);
        AddController(inputGameController);
            
        var carController = new CarController(leftMoveDiff, rightMoveDiff, isStay, prefabsAssetReferences.CarViewReference);
        AddController(carController);

        _inventoryButton = ResourceLoader.LoadAndInstantiateObject<Button>(new ResourcePath() { PathResource = "Prefabs/InventoryButton" }, uiRoot);
        _inventoryButton.onClick.AddListener(OpenInventory);
        AddGameObjects(_inventoryButton.gameObject);

        var abilityRepository = new AbilityRepository(configs, profilePlayer);
        AddController(abilityRepository);
        var abilityView = LoadView<AbilitiesView>(prefabsAssetReferences.AbilitiesViewReference, uiRoot);
        AddGameObjects(abilityView.gameObject);
        var abilitiesController = new AbilitiesController(carController, _inventoryController.Model, abilityRepository,
            abilityView);
        AddController(abilitiesController);

        var battleStartController = CreateBattleStartController(uiRoot, profilePlayer, prefabsAssetReferences.BattleViewReference);
        AddController(battleStartController);
    }

    private T LoadView<T>(AssetReferenceGameObject viewRef, Transform uiRoot) where T: Component, IView
    {
        var result = ResourceLoader.AdressableLoadAndInstantiateView<T>(viewRef, uiRoot);
        _viewsOperationHandle.Add(result.handle);
        return result.view;
    }

    private BattleStartController CreateBattleStartController(Transform uiRoot, ProfilePlayer profilePlayer, AssetReferenceGameObject battleViewReference)
    {
        var battleStartView = LoadView<BattleStartView>(battleViewReference, uiRoot);
        AddGameObjects(battleStartView.gameObject);
        var battleStartController = new BattleStartController(battleStartView, profilePlayer);
        battleStartView.Init(battleStartController.StartBattle);
        return battleStartController;
    }

    private void OpenInventory()
    {
        _inventoryController.ShowInventory();
    }

    protected override void OnDispose()
    {
        foreach (var handle in _viewsOperationHandle)
        {
            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }
        }
        _inventoryButton?.onClick.RemoveAllListeners();
        _inventoryController?.Dispose();
    }
}

