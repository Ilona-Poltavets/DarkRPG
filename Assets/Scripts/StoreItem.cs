using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    [SerializeField] private UI_inventory uiInventory;
    //private Text costText;
    public void Update()
    {
        gameObject.GetComponent<Button_UI>().ClickFunc = () =>
        {
            int cost = System.Int32.Parse(gameObject.GetComponentInChildren<Text>().text);
            uiInventory.BuyItem(gameObject.name,cost);
        };
        gameObject.GetComponent<Button_UI>().MouseRightClickFunc = () =>
        {
            //uiInventory.RemoveEquipmentItem(gameObject.name);
        };
    }
}
