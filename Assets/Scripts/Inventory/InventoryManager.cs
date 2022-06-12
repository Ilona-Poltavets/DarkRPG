using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
[XmlRoot("Inventory")]
public class InventoryManager
{
    public event EventHandler OnItemListChanged;
    [XmlElement("Items")]
    public List<Item> itemList { get; set; }
    [XmlIgnore]
    public Action<Item> useItemAction { get; set; }
    private Player player;
    [XmlElement("Equipment")]
    public SerializableDictionary<string, Item> equipment { get; set; }
    public InventoryManager() { }
    public InventoryManager(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
        equipment = new SerializableDictionary<string, Item>();
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
            player.characteristics.damage -= oldItem.damage;
            player.characteristics.defense -= oldItem.defense;
            AddItem(equipment[item.slot]);
            equipment[item.slot] = item;
        }
        else
        {
            equipment.Add(item.slot, item);
        }
        player.characteristics.damage += item.damage;
        player.characteristics.defense += item.defense;
    }
    public void RemoveEquipment(Item item)
    {
        player.characteristics.damage -= item.damage;
        player.characteristics.defense -= item.defense;
        equipment.Remove(item.slot);
    }
    public int FindHealthPotion()
    {
        foreach(Item item in itemList)
        {
            if(item.itemType == Item.ItemType.HealthPotion)
            {
                RemoveItem(item);
                return (player.characteristics.maxHealth *25)/100;
            }
            else if (item.itemType == Item.ItemType.Medkit)
            {
                RemoveItem(item);
                return (player.characteristics.maxHealth * 50) / 100;
            }
        }
        return 0;
    }
    public SerializableDictionary<string,Item> GetEquipment()
    {
        return equipment;
    }
}
