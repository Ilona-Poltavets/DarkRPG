using UnityEngine;
using UnityEngine.UI;
using Utils;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Class that describes the behavior of the inventory interface
/// </summary>
public class UI_inventory : MonoBehaviour
{
    //Characteristics
    public Text hp;
    public Text damagePoints;
    public Text defencePoints;
    public Text goldCount;

    private InventoryManager inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Player player;

    private Transform EquipmantContainer;
    private Transform EquipmantSlot;

    public void Awake()
    {
        itemSlotContainer = transform.Find("Inventory");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        EquipmantContainer= transform.Find("Equipment");
    }
    private void LateUpdate()
    {
        hp.text = player.currentHealth.ToString();
        defencePoints.text = player.defense.ToString();
        damagePoints.text = player.damage.ToString();
        goldCount.text = player.gold.ToString();
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    public void SetInventory(InventoryManager inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventroyItems();
    }
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventroyItems();
    }
    public void SetEquipment(Item item)
    {
        EquipmantContainer = transform.Find("Equipment");
        Image image;
        switch (item.itemType)
        {
            case Item.ItemType.Ring:
                item.slot = "Ring";
                EquipmantSlot = EquipmantContainer.Find("Ring");
                inventory.AddEquipment(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Bib:
                item.slot = "Bib";
                EquipmantSlot = EquipmantContainer.Find("Bib");
                inventory.AddEquipment(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Helmet:
                item.slot = "Helmet";
                EquipmantSlot = EquipmantContainer.Find("Helmet");
                inventory.AddEquipment(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Sword:
                item.slot = "Sword";
                EquipmantSlot = EquipmantContainer.Find("Sword");
                inventory.AddEquipment(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Necklace:
                item.slot = "Necklace";
                EquipmantSlot = EquipmantContainer.Find("Necklace");
                inventory.AddEquipment(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Shield:
                item.slot = "Shield";
                EquipmantSlot = EquipmantContainer.Find("Shield");
                inventory.AddEquipment(item);
                inventory.RemoveItem(item);
                break;
            case Item.ItemType.Boots:
                item.slot = "Boots";
                EquipmantSlot = EquipmantContainer.Find("Boots");
                inventory.AddEquipment(item);
                inventory.RemoveItem(item);
                break;

        }
        //itemSlotRectTransform = Instantiate(EquipmantSlot, EquipmantContainer).GetComponent<RectTransform>();
        //image = itemSlotRectTransform.GetComponent<Image>();
        image = EquipmantSlot.GetComponent<Image>();
        image.sprite = item.GetSprite();
    }
    /// <summary>
    /// Method for updating elements in UI when they change
    /// </summary>
    /// 
    private void RefreshInventroyItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 85f;
        foreach(Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.transform.position, duplicateItem);
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image=itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            Text uiText = itemSlotRectTransform.Find("amountText").GetComponent<Text>();
            if (item.amount > 1)
            {
                uiText.text = item.amount.ToString();
            }
            else
            {
                uiText.text = "";
            }
            x++;
            if (x > 7)
            {
                x = 0;
                y++;
            }
        }
    }
    public void ToInventory(string name)
    {
        EquipmantSlot = EquipmantContainer.Find(name);
        Image image = EquipmantSlot.GetComponent<Image>();
        image.sprite = null;
        Dictionary<string, Item> equipment = inventory.GetEquipment();
        Item item = equipment[name];
        inventory.RemoveEquipment(item);
        inventory.AddItem(item);
    }
    public void RemoveEquipmentItem(string name)
    {
        EquipmantSlot = EquipmantContainer.Find(name);
        Image image = EquipmantSlot.GetComponent<Image>();
        image.sprite = null;
        Dictionary<string, Item> equipment = inventory.GetEquipment();
        Item item = equipment[name];
        inventory.RemoveEquipment(item);
        ItemWorld.DropItem(player.transform.position, item);
    }
    public void BuyItem(string name, int cost)
    {
        Item item = null;
        switch (name)
        {
            case "HealthPotion":
                item = new Item { itemType = Item.ItemType.HealthPotion, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case "Helmet":
                item = new Item { itemType = Item.ItemType.Helmet, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case "Bib":
                item = new Item { itemType = Item.ItemType.Bib, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case "Boots":
                item = new Item { itemType = Item.ItemType.Boots, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case "Necklace":
                item = new Item { itemType = Item.ItemType.Necklace, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case "Sword":
                item = new Item { itemType = Item.ItemType.Sword, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case "Shield":
                item = new Item { itemType = Item.ItemType.Shield, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case "Medkit":
                item = new Item { itemType = Item.ItemType.Medkit, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case "Ring":
                item = new Item { itemType = Item.ItemType.Ring, amount = 1, slot = "" };
                player.gold -= cost;
                break;
        }
        if (item != null)
            inventory.AddItem(item);
    }
}
