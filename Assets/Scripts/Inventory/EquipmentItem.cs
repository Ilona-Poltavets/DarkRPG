using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Utils;

public class EquipmentItem : MonoBehaviour
{
    //public Image image;
    [SerializeField] private UI_inventory uiInventory;
    public void Update()
    {
        gameObject.GetComponent<Button_UI>().ClickFunc = () =>
        {
            uiInventory.ToInventory(gameObject.name);
        };
        gameObject.GetComponent<Button_UI>().MouseRightClickFunc = () =>
        {
           uiInventory.RemoveEquipmentItem(gameObject.name);
        };
    }
}

