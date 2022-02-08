using System.Collections.Generic;
using UnityEngine;

public class InventoryView : IInventoryView
{
    public void Display(IReadOnlyList<IItem> items)
    {
        foreach(var item in items)
            Debug.Log($"Id item: {item.Id}. Title item: {item.Info.Title}");
    }
}
