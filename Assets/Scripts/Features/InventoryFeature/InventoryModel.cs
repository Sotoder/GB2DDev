using System.Collections.Generic;
using Tools;

public class InventoryModel : IInventoryModel
{
    private readonly List<IItem> _items = new List<IItem>();

    private List<UpgradeItemConfig> _upgrades = new List<UpgradeItemConfig>();

    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _items;
    }

    public IReadOnlyList<UpgradeItemConfig> GetUpgrades()
    {
        return _upgrades;
    }

    public void EquipBaseItem(IItem item)
    {
        if (_items.Contains(item))
            return;

        _items.Add(item);
    }

    public void UnEquipItem(IItem item)
    {
        _items.Remove(item);
    }

    public void UpdateUpgradesList (List<UpgradeItemConfig> upgradeItems)
    {
        _upgrades.Clear();
        _upgrades.AddRange(upgradeItems);
    }
}
