using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
namespace MyProject
{
    /// <summary>
    /// Item behavior class on the map
    /// </summary>
    public class ItemWorld : MonoBehaviour
    {
        /// <summary>
        /// Camera position
        /// </summary>
        public Transform cam;
        //private Transform parentT;
        private Transform tr;
        /// <summary>
        /// Item generation at spawn point
        /// </summary>
        /// <param name="position">Spawner position</param>
        /// <param name="item">Item to generate</param>
        /// <returns>Item in the game world with its position</returns>
        public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
        {
            Transform transform = Instantiate(ItemAssets.Instance.ItemWorld, position, Quaternion.identity);
            ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
            itemWorld.SetItem(item);
            return itemWorld;
        }
        private Item item;
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            //parentT = transform.parent;
            tr = transform;
        }
        private void Update()
        {
            tr.rotation = Quaternion.LookRotation(cam.position - tr.position);
        }
        /// <summary>
        /// Setting the image of the subject
        /// </summary>
        /// <param name="item">Item to be set image</param>
        public void SetItem(Item item)
        {
            this.item = item;
            spriteRenderer.sprite = item.GetSprite();
        }
        public Item GetItem()
        {
            return item;
        }
        /// <summary>
        /// Item generation on the map
        /// </summary>
        /// <param name="dropPosition">The position where the item will be generated</param>
        /// <param name="item">Item to be generated</param>
        /// <returns>Item in position</returns>
        public static ItemWorld DropItem(Vector3 dropPosition, Item item)
        {
            Vector3 randomDir = UtilsClass.GetRandomDir();
            randomDir.y = 1f;
            ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 2f, item);
            return itemWorld;
        }
        /// <summary>
        /// Removing an item from the map
        /// </summary>
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}