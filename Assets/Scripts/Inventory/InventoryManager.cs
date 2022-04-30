using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;

    private Dictionary<string, Item> equipment;
    public InventoryManager(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
        equipment = new Dictionary<string, Item>();
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount=1 });
        AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1,damage=50 });
        AddItem(new Item { itemType = Item.ItemType.Shield, amount = 1,defense=30 });
    }
    public void AddItem(Item item)
    {
        int cost = (item.damage + item.defense) * 2 + 10;
        item.cost = cost;
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach(Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    //inventoryItem.itemType -= item.amount;//bug for infinity drop
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void UseItem(Item item)
    {
        useItemAction(item);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
    public void AddEquipment(Item item)
    {
        if (equipment.ContainsKey(item.slot))
        {
            AddItem(equipment[item.slot]);
            equipment[item.slot] = item;
        }
        else
        {
            equipment.Add(item.slot, item);
        }
    }
    public void RemoveEquipment(Item item)
    {
        equipment.Remove(item.slot);
    }
    public Dictionary<string,Item> GetEquipment()
    {
        return equipment;
    }
}
