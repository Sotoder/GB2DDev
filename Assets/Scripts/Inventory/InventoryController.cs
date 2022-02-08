using System.Collections.Generic;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private readonly IItemsRepository _itemsRepository;

    public InventoryController(List<ItemConfig> itemConfigs, InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;
        _inventoryView = new InventoryView();
        _itemsRepository = new ItemsRepository(itemConfigs);
    }

    public void ShowInventory()
    {
        foreach (var item in _itemsRepository.Items.Values)
            _inventoryModel.EquipItem(item);

        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryView.Display(equippedItems);
    }
}
