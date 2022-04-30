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
    [SerializeField] private Tooltip tooltip;

    private Player player;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

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
                if (Player.onShop)
                    SellItem(item);
                else
                    inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.transform.position, duplicateItem);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseOverOnceFunc = () => tooltip.ShowTooltip(item.ToString());
            itemSlotRectTransform.GetComponent<Button_UI>().MouseOutOnceFunc = () => tooltip.HideTooltip();

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
    public void SellItem(Item item)
    {
        inventory.RemoveItem(item);
        player.gold += item.cost / 2;
    }
}
