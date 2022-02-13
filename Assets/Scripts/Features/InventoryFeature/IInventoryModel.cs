using System.Collections.Generic;
using Tools;

public interface IInventoryModel
{
    IReadOnlyList<IItem> GetEquippedItems();
    IReadOnlyList<UpgradeItemConfig> GetUpgrades();
    void EquipBaseItem(IItem item);
    void UnEquipItem(IItem item);
    void UpdateUpgradesList(List<UpgradeItemConfig> upgradeItems);
}
