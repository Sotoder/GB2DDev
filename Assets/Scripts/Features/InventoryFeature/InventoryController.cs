﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;
using UnityEngine.Events;

public class InventoryController : BaseController, IInventoryController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Inventory" };
    private readonly IInventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private readonly IRepository<int, IItem> _itemsRepository;

    public UnityAction CloseAndSaveInventory;
    public IInventoryModel Model => _inventoryModel;

    public InventoryController(List<ItemConfig> itemConfigs, IReadOnlyList<UpgradeItemConfig> upgradeItems, Transform placeForUi, InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;
        _inventoryView = ResourceLoader.LoadAndInstantiateView<InventoryView>(_viewPath, placeForUi);
        AddGameObjects(_inventoryView.gameObject);
        _inventoryView.Init(upgradeItems, inventoryModel);
        _inventoryView.UpgradeSaved += _inventoryModel.UpdateUpgradesList;
        _inventoryView.UpgradeSaved += InvetoryClosed;

        _itemsRepository = new ItemsRepository(itemConfigs);

        EquipBaseItems();
    }

    private void EquipBaseItems()
    {
        foreach (var item in _itemsRepository.Content.Values)
            if (_inventoryModel.GetEquippedItems().FirstOrDefault(x => x.Id == item.Id) == null)
            {
                _inventoryModel.EquipBaseItem(item);
            }
    }

    private void InvetoryClosed(List<UpgradeItemConfig> upgradesList)
    {
        CloseAndSaveInventory?.Invoke();
    }

    public void ShowInventory()
    {
        _inventoryView.Show();
    }

    public void DisplayEquippedItems()
    {
        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryView.Display(equippedItems);
    }

    public void SetInventoryViewPosition(Transform plasceForUi)
    {
        _inventoryView.gameObject.transform.SetParent(plasceForUi);
        _inventoryView.gameObject.transform.position = plasceForUi.position;
    }

    public void SetOnGameSceneFlag(bool isOnScene)
    {
        _inventoryView.SetOnGameSceneFlag(isOnScene);
    }

    public new void OnDispose()
    {
        _inventoryView.UpgradeSaved -= _inventoryModel.UpdateUpgradesList;
        base.OnDispose();
    }
}
