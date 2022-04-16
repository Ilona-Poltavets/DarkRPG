using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
        ManaPotion,
        Medkit,
        Shield,
    }
    public ItemType itemType;
    public int amount;
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:
                return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion:
                return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:
                return ItemAssets.Instance.manaPotionSprite;
            case ItemType.Medkit:
                return ItemAssets.Instance.medkitSprite;
            case ItemType.Shield:
                return ItemAssets.Instance.shildSprite;
        }
    }
}
