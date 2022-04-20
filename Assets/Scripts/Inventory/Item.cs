using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
[Serializable]
public class Item
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
        ManaPotion,
        Medkit,
        Shield,
        Ring,
        Necklace,
        Bib,
        Bow,
        Helmet,
        Boots,
    }
    public ItemType itemType;
    public int amount;
    public string slot = "";
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
            case ItemType.Ring:
                return ItemAssets.Instance.ringSprite;
            case ItemType.Necklace:
                return ItemAssets.Instance.necklaceSprite;
            case ItemType.Bib:
                return ItemAssets.Instance.bibSprite;
            case ItemType.Bow:
                return ItemAssets.Instance.bowSprite;
            case ItemType.Helmet:
                return ItemAssets.Instance.helmetSprite;
            case ItemType.Boots:
                return ItemAssets.Instance.bootsSprite;
        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.ManaPotion:
            case ItemType.Medkit:
            case ItemType.HealthPotion:
                return true;
            case ItemType.Sword:
            case ItemType.Shield:
            case ItemType.Ring:
            case ItemType.Necklace:
            case ItemType.Bib:
            case ItemType.Bow:
            case ItemType.Helmet:
            case ItemType.Boots:
                return false;
        }
    }
}