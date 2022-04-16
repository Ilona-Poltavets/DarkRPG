using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    public InventoryManager()
    {
        itemList = new List<Item>();
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount=1 });
        AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Shield, amount = 1 });
    }
    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
