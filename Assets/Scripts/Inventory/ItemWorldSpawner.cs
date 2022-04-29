using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class for descriptions behavior the appearance of loot in the game world
/// </summary>
public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;
    /// <summary>
    /// Loot generation at scene start
    /// </summary>
    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
    public static Item GenerateGold()
    {
        int count = Random.Range(2, 10);
        Item item = new Item { itemType = Item.ItemType.Gold, amount = count, slot = "" };
        return item;
    }
}
