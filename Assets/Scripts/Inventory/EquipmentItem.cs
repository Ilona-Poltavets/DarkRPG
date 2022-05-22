using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Utils;
namespace MyProject
{
    /// <summary>
    /// Item behavior class in equipment
    /// </summary>
    public class EquipmentItem : MonoBehaviour
    {
        //public Image image;
        [SerializeField] private UI_inventory uiInventory;
        /// <summary>
        /// The method is executed when drawing each frame.
        /// It monitors the actions of the player in the inventory menu,
        /// if the player clicks on the item with the right button of the mouse,
        /// then the item is removed from the outfit and goes into the inventory,
        /// if the right one, the item will be thrown
        /// </summary>
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
}