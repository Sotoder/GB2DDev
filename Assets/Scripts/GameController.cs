using System;
using System.Collections.Generic;
using Features.AbilitiesFeature;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class GameController : BaseController
{
    private Button _inventoryButton;
    private InventoryController _inventoryController;
    public GameController(ProfilePlayer profilePlayer, IReadOnlyList<AbilityItemConfig> configs,
                          List<ItemConfig> itemsConfig, IReadOnlyList<UpgradeItemConfig> upgradeItems, InventoryModel inventoryModel, Transform uiRoot)
    {
        _inventoryController = new InventoryController(itemsConfig, upgradeItems, uiRoot, inventoryModel);
        _inventoryController.SetOnGameSceneFlag(true);

        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        var isStay = new SubscriptionProperty<bool>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
        AddController(tapeBackgroundController);
        
        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, isStay, profilePlayer.CurrentCar);
        AddController(inputGameController);
            
        var carController = new CarController(leftMoveDiff, rightMoveDiff, isStay);
        AddController(carController);

        _inventoryButton = ResourceLoader.LoadAndInstantiateObject<Button>(new ResourcePath() { PathResource = "Prefabs/InventoryButton" }, uiRoot);
        _inventoryButton.onClick.AddListener(OpenInventory);
        AddGameObjects(_inventoryButton.gameObject);

        var abilityRepository = new AbilityRepository(configs, profilePlayer);
        var abilityView =
            ResourceLoader.LoadAndInstantiateView<AbilitiesView>(
                new ResourcePath() { PathResource = "Prefabs/AbilitiesView" }, uiRoot);
        AddGameObjects(abilityView.gameObject);
        var abilitiesController = new AbilitiesController(carController, _inventoryController.Model, abilityRepository,
            abilityView);
        AddController(abilitiesController);

        var battleStartController = CreateBattleStartController(uiRoot, profilePlayer);
        AddController(battleStartController);
    }

    private BattleStartController CreateBattleStartController(Transform uiRoot, ProfilePlayer profilePlayer)
    {
        var battleStartView = ResourceLoader.LoadAndInstantiateView<BattleStartView>(new ResourcePath { PathResource = "Prefabs/BattleStartView" }, uiRoot);
        AddGameObjects(battleStartView.gameObject);
        var battleStartController = new BattleStartController(battleStartView, profilePlayer);
        battleStartView.Init(battleStartController.StartBattle);
        return battleStartController;
    }

    private void OpenInventory()
    {
        _inventoryController.ShowInventory();
    }

    public new void OnDispose()
    {
        _inventoryButton.onClick.RemoveAllListeners();
        _inventoryController.Dispose();
        base.OnDispose();
    }
}

