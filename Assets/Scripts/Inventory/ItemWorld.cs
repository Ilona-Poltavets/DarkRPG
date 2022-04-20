using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class ItemWorld : MonoBehaviour
{
    public Transform cam;
    //private Transform parentT;
    private Transform tr;
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
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }
    public Item GetItem()
    {
        return item;
    }
    public static ItemWorld DropItem(Vector3 dropPosition,Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 2f, item);
        return itemWorld;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
