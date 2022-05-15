using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;
    private Player player;

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
    public void SetPlayer(Player player)
    {
        this.player = player;
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
            Item oldItem = equipment[item.slot];
            player.damage -= oldItem.damage;
            player.defense -= oldItem.defense;
            AddItem(equipment[item.slot]);
            equipment[item.slot] = item;
        }
        else
        {
            equipment.Add(item.slot, item);
        }
        player.damage += item.damage;
        player.defense += item.defense;
    }
    public void RemoveEquipment(Item item)
    {
        player.damage -= item.damage;
        player.defense -= item.defense;
        equipment.Remove(item.slot);
    }
    public int FindHealthPotion()
    {
        foreach(Item item in itemList)
        {
            if(item.itemType == Item.ItemType.HealthPotion)
            {
                RemoveItem(item);
                return (player.maxHealth*25)/100;
            }
            else if (item.itemType == Item.ItemType.Medkit)
            {
                RemoveItem(item);
                return (player.maxHealth * 50) / 100;
            }
        }
        return 0;
    }
    public Dictionary<string,Item> GetEquipment()
    {
        return equipment;
    }
}
