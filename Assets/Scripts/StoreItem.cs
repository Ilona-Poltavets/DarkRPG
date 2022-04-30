using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    private InventoryManager inventory;
    [SerializeField]
    private UI_inventory uiInventory;
    private List<Item> rangeItems;
    private Player player;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    //private Text costText;
    private void Awake()
    {
        itemSlotContainer = transform.Find("Items");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        //RefreshInventroyItems();
    }
    public void SetInventory(InventoryManager inventory)
    {
        rangeItems = new List<Item>();
        this.inventory = inventory;
        AddItemsInShop();
        RefreshInventroyItems();
    }
    private void RefreshInventroyItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;
        foreach (Item item in rangeItems)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                int cost = System.Int32.Parse(itemSlotRectTransform.GetComponentInChildren<Text>().text);
                BuyItem(item, item.cost);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(item.ToString());
            itemSlotRectTransform.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();

            itemSlotRectTransform.anchoredPosition = new Vector2(x * (itemSlotCellSize + 15), -y * (itemSlotCellSize + 50));
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            Text uiText = itemSlotRectTransform.Find("cost").GetComponent<Text>();
            uiText.text = item.cost.ToString();
            x++;
            if (x > 5)
            {
                x = 0;
                y++;
            }
        }
    }
    public void Update()
    {
        //RefreshInventroyItems();//так делать нельзя
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    private void AddItemsInShop()
    {
        Dictionary<string, Item> equip = new Dictionary<string, Item>();
        equip = inventory.GetEquipment();
        int damage = 0;
        int defence = 0;
        int cost = 10;
        rangeItems.Add(new Item { itemType = Item.ItemType.HealthPotion, amount = 1, cost = cost });

        damage = (equip.ContainsKey("Sword") ? equip["Sword"].damage : 0) + (player.lvl * 5);
        cost = (damage + 0) * 2;
        rangeItems.Add(new Item { itemType = Item.ItemType.Sword, amount = 1, cost = cost, damage=damage });

        defence = (equip.ContainsKey("Shield") ? equip["Shield"].defense : 0) + (player.lvl * 2);
        cost = (0 + defence) * 2 + 10;
        rangeItems.Add(new Item { itemType = Item.ItemType.Shield, amount = 1, cost = cost, defense=defence });

        defence = (equip.ContainsKey("Bib") ? equip["Bib"].defense : 0) + (player.lvl * 2);
        cost = (0 + defence) * 2 + 10;
        rangeItems.Add(new Item { itemType = Item.ItemType.Bib, amount = 1, cost = cost, defense = defence });

        cost = 30;
        rangeItems.Add(new Item { itemType = Item.ItemType.Medkit, amount = 1, cost = cost, damage = damage });

        damage = (equip.ContainsKey("Necklace") ? equip["Necklace"].damage : 0) + (player.lvl * 5); ;
        cost = (damage + 0) * 2;
        rangeItems.Add(new Item { itemType = Item.ItemType.Necklace, amount = 1, cost = cost, damage = damage });

        defence = (equip.ContainsKey("Boots") ? equip["Boots"].defense : 0) + (player.lvl * 2);
        cost = (0 + defence) * 2 + 10;
        rangeItems.Add(new Item { itemType = Item.ItemType.Boots, amount = 1, cost = cost, defense = defence });

        damage = (equip.ContainsKey("Ring") ? equip["Ring"].damage : 0) + (player.lvl * 5); ;
        cost = (damage + 0) * 2;
        rangeItems.Add(new Item { itemType = Item.ItemType.Ring, amount = 1, cost = cost, damage = damage });

        defence = (equip.ContainsKey("Helmet") ? equip["Helmet"].defense : 0) + (player.lvl * 2);
        cost = (0 + defence) * 2 + 10;
        rangeItems.Add(new Item { itemType = Item.ItemType.Helmet, amount = 1, cost = cost, defense = defence });
    }
    public void BuyItem(Item item, int cost)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                item = new Item { itemType = Item.ItemType.HealthPotion, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case Item.ItemType.Helmet:
                item = new Item { itemType = Item.ItemType.Helmet, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case Item.ItemType.Bib:
                item = new Item { itemType = Item.ItemType.Bib, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case Item.ItemType.Boots:
                item = new Item { itemType = Item.ItemType.Boots, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case Item.ItemType.Necklace:
                item = new Item { itemType = Item.ItemType.Necklace, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case Item.ItemType.Sword:
                item = new Item { itemType = Item.ItemType.Sword, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case Item.ItemType.Shield:
                item = new Item { itemType = Item.ItemType.Shield, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case Item.ItemType.Medkit:
                item = new Item { itemType = Item.ItemType.Medkit, amount = 1, slot = "" };
                player.gold -= cost;
                break;
            case Item.ItemType.Ring:
                item = new Item { itemType = Item.ItemType.Ring, amount = 1, slot = "" };
                player.gold -= cost;
                break;
        }
        if (item != null)
            inventory.AddItem(item);
    }
}
