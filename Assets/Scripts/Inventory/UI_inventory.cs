using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_inventory : MonoBehaviour
{
    private InventoryManager inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public void Awake()
    {
        itemSlotContainer = transform.Find("Inventory");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
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
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image=itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x > 7)
            {
                x = 0;
                y++;
            }
        }
    }
}
