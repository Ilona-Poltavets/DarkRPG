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
        Medkit,
        Shield,
        Ring,
        Necklace,
        Bib,
        Helmet,
        Boots,
        Gold,
    }
    public ItemType itemType;
    public int amount;
    public string slot = "";
    public int cost = 0;
    public int damage=0;
    public int defense = 0;
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:
                return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion:
                return ItemAssets.Instance.healthPotionSprite;
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
            case ItemType.Helmet:
                return ItemAssets.Instance.helmetSprite;
            case ItemType.Boots:
                return ItemAssets.Instance.bootsSprite;
            case ItemType.Gold:
                return ItemAssets.Instance.goldSprite;
        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Medkit:
            case ItemType.HealthPotion:
                return true;
            case ItemType.Sword:
            case ItemType.Shield:
            case ItemType.Ring:
            case ItemType.Necklace:
            case ItemType.Bib:
            case ItemType.Helmet:
            case ItemType.Boots:
                return false;
        }
    }
    public override string ToString()
    {
        return $"***** {this.itemType} *****\nDamage: {this.damage}\nDefence: {this.defense}\nCost: {this.cost}";
    }
}