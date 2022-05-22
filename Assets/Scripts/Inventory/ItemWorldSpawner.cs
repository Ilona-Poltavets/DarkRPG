using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject
{
    /// <summary>
    /// Class for descriptions behavior the appearance of loot in the game world
    /// </summary>
    public class ItemWorldSpawner : MonoBehaviour
    {
        /// <summary>
        /// Item that will appear on the map
        /// </summary>
        public Item item;
        private void Start()
        {
            ItemWorld.SpawnItemWorld(transform.position, item);
            Destroy(gameObject);
        }
        /// <summary>
        /// Method to generate amount of gold
        /// </summary>
        /// <returns>Generated Item Object</returns>
        public static Item GenerateGold()
        {
            int count = Random.Range(2, 10);
            Item item = new Item { itemType = Item.ItemType.Gold, amount = count, slot = "" };
            return item;
        }
    }
}