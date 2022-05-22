using UnityEngine;
using UnityEngine.UI;
using Utils;
using System.Collections;
using System.Collections.Generic;
namespace MyProject
{
    /// <summary>
    /// Class that describes the behavior of the inventory interface
    /// </summary>
    public class UI_inventory : MonoBehaviour
    {
        //Characteristics
        /// <summary>
        /// Text to display current health
        /// </summary>
        public Text hp;
        /// <summary>
        /// Text to display the current damage the player can deal
        /// </summary>
        public Text damagePoints;
        /// <summary>
        /// Text to display the player's current armor
        /// </summary>
        public Text defencePoints;
        /// <summary>
        /// Text to display the player's gold amount
        /// </summary>
        public Text goldCount;

        private InventoryManager inventory;
        [SerializeField] private Tooltip tooltip;

        private Player player;

        private Transform itemSlotContainer;
        private Transform itemSlotTemplate;

        private Transform EquipmantContainer;
        private Transform EquipmantSlot;
        /// <summary>
        /// The method is called before the first frame of the game.
        /// It searches for templates and interface elements on the screen
        /// </summary>
        public void Awake()
        {
            itemSlotContainer = transform.Find("Inventory");
            itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

            EquipmantContainer = transform.Find("Equipment");
        }
        private void LateUpdate()
        {
            hp.text = player.currentHealth.ToString();
            defencePoints.text = player.defense.ToString();
            damagePoints.text = player.damage.ToString();
            goldCount.text = player.gold.ToString();
        }
        /// <summary>
        /// Setting the current player for which the characteristics will be displayed
        /// </summary>
        /// <param name="player">Player object on the map</param>
        public void SetPlayer(Player player)
        {
            this.player = player;
        }
        /// <summary>
        /// Obtaining a Player's Inventory
        /// </summary>
        /// <param name="inventory">Inventory manager</param>
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
        /// <summary>
        /// Method of setting an item from inventory as equipment
        /// </summary>
        /// <param name="item">The item the user clicked on</param>
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
        private void RefreshInventroyItems()
        {
            foreach (Transform child in itemSlotContainer)
            {
                if (child == itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }
            int x = 0;
            int y = 0;
            float itemSlotCellSize = 85f;
            foreach (Item item in inventory.GetItemList())
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
                Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();
                Text uiText = itemSlotRectTransform.Find("amountText").GetComponent<Text>();
                uiText.text = item.amount.ToString();
                x++;
                if (x > 7)
                {
                    x = 0;
                    y++;
                }
            }
        }
        /// <summary>
        /// Method for removing an item from equipment to inventory
        /// </summary>
        /// <param name="name">The name of the item to be returned to inventory</param>
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
        /// <summary>
        /// Method that allows you to drop an item from inventory
        /// </summary>
        /// <param name="name">The name of the item to be discarded</param>
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
        /// <summary>
        /// Method of selling an item from inventory
        /// </summary>
        /// <param name="item">Item for sale</param>
        public void SellItem(Item item)
        {
            inventory.RemoveItem(item);
            player.gold += item.cost / 2;
        }
    }
}