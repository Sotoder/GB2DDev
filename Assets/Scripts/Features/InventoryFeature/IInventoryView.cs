using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

public interface IInventoryView:IView
{
    void Init(IReadOnlyList<UpgradeItemConfig> upgradeItems, InventoryModel inventoryModel);
    void Display(IReadOnlyList<IItem> items);
    UnityAction<List<UpgradeItemConfig>> UpgradeSaved { get; set; }
    GameObject gameObject { get; }
    void SetOnGameSceneFlag(bool isOnScene);
}
