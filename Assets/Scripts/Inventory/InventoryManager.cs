using System;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject
{
    /// <summary>
    /// Inventory behavior class and items in it
    /// </summary>
    public class InventoryManager
    {
        /// <summary>
        /// Checking if the list of items in the inventory has changed
        /// </summary>
        public event EventHandler OnItemListChanged;
        /// <summary>
        /// List of items in inventory
        /// </summary>
        protected List<Item> itemList;
        private Action<Item> useItemAction;
        private Player player;

        /// <summary>
        /// Equipment dictionary key is the name of the slot, and the value is the item
        /// </summary>
        private Dictionary<string, Item> equipment;
        /// <summary>
        /// The constructor in which the inventory, equipment is initialized. Also 3 default items
        /// </summary>
        /// <param name="useItemAction"></param>
        public InventoryManager(Action<Item> useItemAction)
        {
            this.useItemAction = useItemAction;
            itemList = new List<Item>();
            equipment = new Dictionary<string, Item>();
            AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
            AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1, damage = 50 });
            AddItem(new Item { itemType = Item.ItemType.Shield, amount = 1, defense = 30 });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        public void SetPlayer(Player player)
        {
            this.player = player;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
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
        /// <summary>
        /// Removing an item from the inventory list
        /// </summary>
        /// <param name="item">Item to be removed</param>
        public void RemoveItem(Item item)
        {
            if (item.IsStackable())
            {
                Item itemInInventory = null;
                foreach (Item inventoryItem in itemList)
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
        /// <summary>
        /// Starting an action to use an item
        /// </summary>
        /// <param name="item">Item to be used</param>
        public void UseItem(Item item)
        {
            useItemAction(item);
        }
        /// <summary>
        /// Submit inventory list
        /// </summary>
        /// <returns>Inventory list</returns>
        public List<Item> GetItemList()
        {
            return itemList;
        }
        /// <summary>
        /// Adding an Item to the Equipment Dictionary
        /// </summary>
        /// <param name="item">The item to be moved into the outfit</param>
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
        /// <summary>
        /// Removing an item from equipment
        /// </summary>
        /// <param name="item">Item to be removed from equipment</param>
        public void RemoveEquipment(Item item)
        {
            player.damage -= item.damage;
            player.defense -= item.defense;
            equipment.Remove(item.slot);
        }
        /// <summary>
        /// Finding a first-aid kit or a healing potion in the inventory, if there is one, it is used
        /// </summary>
        /// <returns>In the presence of a first-aid kit or a potion, the player’s health is replenished, otherwise 0</returns>
        public int FindHealthPotion()
        {
            foreach (Item item in itemList)
            {
                if (item.itemType == Item.ItemType.HealthPotion)
                {
                    RemoveItem(item);
                    return (player.maxHealth * 25) / 100;
                }
                else if (item.itemType == Item.ItemType.Medkit)
                {
                    RemoveItem(item);
                    return (player.maxHealth * 50) / 100;
                }
            }
            return 0;
        }
        /// <summary>
        /// Equipment output
        /// </summary>
        /// <returns>Equipment</returns>
        public Dictionary<string, Item> GetEquipment()
        {
            return equipment;
        }
    }
}