using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
namespace MyProject
{
    /// <summary>
    /// Class for creating items
    /// </summary>
    [Serializable]
    public class Item
    {
        /// <summary>
        /// A set of types of items that can be in the game
        /// </summary>
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
        /// <summary>
        /// Item type field
        /// </summary>
        public ItemType itemType;
        /// <summary>
        /// Number of items if the item is stackable or if it is gold
        /// </summary>
        public int amount;
        /// <summary>
        /// Slot, if the item is in equipment, then it is indicated in which slot it is
        /// </summary>
        public string slot = "";
        /// <summary>
        /// Item cost
        /// </summary>
        public int cost = 0;
        /// <summary>
        /// Damage that is added to the player's damage stats when the item is equipped
        /// </summary>
        public int damage = 0;
        /// <summary>
        /// Armor that is added to the player's armor stats when the item is equipped
        /// </summary>
        public int defense = 0;
        /// <summary>
        /// Get item sprite by item type
        /// </summary>
        /// <returns>Item sprite</returns>
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
        /// <summary>
        /// Checking if an item stacks
        /// </summary>
        /// <returns>If the item stacks, then it's true, otherwise it doesn't.</returns>
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
        /// <summary>
        /// Generating an item string with its characteristics
        /// </summary>
        /// <returns>Item characteristics</returns>
        public override string ToString()
        {
            return $"***** {this.itemType} *****\nDamage: {this.damage}\nDefence: {this.defense}\nCost: {this.cost}";
        }
    }
}