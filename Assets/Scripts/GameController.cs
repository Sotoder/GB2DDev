using System;
using System.Collections.Generic;
using Features.AbilitiesFeature;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class GameController : BaseController
{
    private Button _inventoryButton;
    private IInventoryController _inventoryController;
    public GameController(ProfilePlayer profilePlayer, IReadOnlyList<AbilityItemConfig> configs, InventoryController inventoryController, Transform uiRoot)
    {
        _inventoryController = inventoryController;

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

        var abilityRepository = new AbilityRepository(configs, profilePlayer);
        var abilityView =
            ResourceLoader.LoadAndInstantiateView<AbilitiesView>(
                new ResourcePath() { PathResource = "Prefabs/AbilitiesView" }, uiRoot);
        var abilitiesController = new AbilitiesController(carController, inventoryController.Model, abilityRepository,
            abilityView);
        AddController(abilitiesController);
    }

    private void OpenInventory()
    {
        _inventoryController.ShowInventory();
    }

    public new void OnDispose()
    {
        _inventoryButton.onClick.RemoveAllListeners();
        base.OnDispose();
    }
}

