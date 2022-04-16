using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_inventory : MonoBehaviour
{
    private InventoryManager inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public void Start()
    {
        itemSlotContainer = transform.Find("Inventory");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        Debug.Log(itemSlotTemplate);
        Debug.Log(itemSlotContainer);
    }
    public void SetInventory(InventoryManager inventory)
    {
        this.inventory = inventory;
        RefreshInventroyItems();
    }
    private void RefreshInventroyItems()
    {
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
