using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using UnityEngine.UI;
using System.Timers;
using System.Collections;
namespace MyProject
{
	/// <summary>
	/// Player behavior class
	/// </summary>
	public class Player : MonoBehaviour
	{
		/// <summary>
		/// Health Bar Object
		/// </summary>
		[SerializeField] private HealthBar healthBar;
		/// <summary>
		/// Experience Bar Object
		/// </summary>
		[SerializeField] private ExpBar expBar;
		/// <summary>
		/// Animator object for animations
		/// </summary>
		private Animator animator;

		//Characteristics
		/// <summary>
		/// Maximum health
		/// </summary>
		public int maxHealth;
		/// <summary>
		/// Current health
		/// </summary>
		public int currentHealth;
		/// <summary>
		/// Current level
		/// </summary>
		public int lvl = 1;
		/// <summary>
		/// Experience
		/// </summary>
		public int exp;
		/// <summary>
		/// Armor
		/// </summary>
		public int defense;
		/// <summary>
		/// Damage
		/// </summary>
		public int damage;
		/// <summary>
		/// Amount of gold
		/// </summary>
		public int gold;

		/// <summary>
		/// Inventory management object
		/// </summary>
		private InventoryManager inventory;
		/// <summary>
		/// Interface for displaying inventory
		/// </summary>
		[SerializeField] private UI_inventory uiInventory;
		/// <summary>
		/// Interface for displaying the store
		/// </summary>
		[SerializeField] private StoreItem uiStore;
		/// <summary>
		/// Popup text when level up
		/// </summary>
		[SerializeField] private Text textLvlUp;
		/// <summary>
		/// Popup text when player dies
		/// </summary>
		[SerializeField] private GameObject dieText;
		/// <summary>
		/// Variable determining whether the player is in the store
		/// </summary>
		public static bool onShop = false;
		private void Awake()
		{
			Cursor.visible = false;
			maxHealth = 100;
			exp = 10;
			lvl = 1;
			defense = 10;
			damage = 10;
			gold = 1000;
		}
		void Start()
		{
			animator = GetComponent<Animator>();
			currentHealth = maxHealth;
			healthBar.SetMaxHealth(maxHealth);

			expBar.SetLevel(lvl);
			expBar.SetExp(exp);

			inventory = new InventoryManager(UseItem);
			inventory.SetPlayer(this);
			uiInventory.SetPlayer(this);
			uiInventory.SetInventory(inventory);
			uiInventory.gameObject.SetActive(false);
			uiStore.SetPlayer(this);
			uiStore.SetInventory(inventory);
			uiStore.gameObject.SetActive(false);
		}
		/// <summary>
		/// Using a thing from an inanentor.
		/// </summary>
		/// <param name="item">Used item</param>
		public void UseItem(Item item)
		{
			switch (item.itemType)
			{
				case Item.ItemType.HealthPotion:
					Healer(10);
					inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
					break;
				case Item.ItemType.Medkit:
					Healer(30);
					inventory.RemoveItem(new Item { itemType = Item.ItemType.Medkit, amount = 1 });
					break;
				default:
					uiInventory.SetEquipment(item);
					uiStore.RefreshInventroyItems();
					break;
			}
		}
		void FixedUpdate()
		{
			if (exp > 1000)
			{
				LevelUp();
			}
			if (currentHealth <= 0)
			{
				StartCoroutine(DeathCoroutine());
			}
			if (Keyboard.current[Key.E].wasPressedThisFrame)
			{
				if (onShop)
				{
					Resume();
				}
			}
			if (Keyboard.current[Key.Q].wasPressedThisFrame && (inventory.FindHealthPotion() != 0))
			{
				Healer(inventory.FindHealthPotion());
			}
		}
		/// <summary>
		/// Method for taking damage from enemies, the damage taken depends on the armor and the player
		/// </summary>
		/// <param name="damage">Damage dealt by enemy</param>
		public void TakeDamage(int damage)
		{
			currentHealth -= (damage - defense);
			healthBar.SetHealth(currentHealth);
		}
		/// <summary>
		/// Method for healing, replenishes the player's health
		/// </summary>
		/// <param name="points">Health points obtained from the use of a first-aid kit or a healing potion</param>
		public void Healer(int points)
		{
			currentHealth += points;
			healthBar.SetHealth(currentHealth);
		}
		/// <summary>
		/// Method of adding experience
		/// </summary>
		/// <param name="points">The amount of experience gained</param>
		public void AddExp(int points)
		{
			exp += points;
			expBar.SetExp(exp);
		}
		IEnumerator TextCoroutine(string text)
		{
			textLvlUp.text = "";
			foreach (char c in text)
			{
				textLvlUp.text += c;
				yield return new WaitForSecondsRealtime(0.1f);
			}
			textLvlUp.text = "";
		}
		IEnumerator DeathCoroutine()
		{
			animator.SetTrigger("death");
			dieText.SetActive(true);
			yield return new WaitForSecondsRealtime(3f);
			Application.LoadLevel(Application.loadedLevel);
		}
		private void LevelUp()
		{
			StartCoroutine(TextCoroutine("★★★ LEVEL UP ★★★"));
			float rand = Random.RandomRange(0f, 5f);
			if (animator)
			{
				if (rand <= 1)
					animator.SetTrigger("levelUp1");
				else if (rand <= 2)
					animator.SetTrigger("levelUp2");
				else if (rand <= 3)
					animator.SetTrigger("levelUp3");
				else if (rand <= 4)
					animator.SetTrigger("levelUp4");
				else if (rand <= 5)
					animator.SetTrigger("levelUp5");
			}
			lvl += 1;
			exp -= 1000;
			float k1 = maxHealth * 0.45f;
			maxHealth += 100 + Mathf.RoundToInt(k1);
			currentHealth = maxHealth;
			defense += 10;
			damage += 12;
			healthBar.SetMaxHealth(maxHealth);
			expBar.SetExp(exp);
			expBar.SetLevel(lvl);
		}
		private void OnTriggerEnter(Collider other)
		{
			ItemWorld itemWorld = other.GetComponent<ItemWorld>();
			if (itemWorld != null)
			{
				if (itemWorld.GetItem().itemType == Item.ItemType.Gold)
				{
					gold += itemWorld.GetItem().amount;
					itemWorld.DestroySelf();
				}
				if (inventory.itemList.Count < 56 && itemWorld.GetItem().itemType != Item.ItemType.Gold)
				{
					inventory.AddItem(itemWorld.GetItem());
					itemWorld.DestroySelf();
				}
			}
		}
		private void OnTriggerStay(Collider other)
		{
			if (other.tag == "Shop")
			{
				if (Keyboard.current[Key.E].wasPressedThisFrame)
				{
					if (!onShop)
					{
						Pause();
					}
				}
			}
		}
		/// <summary>
		/// Resuming the game method, the game speed becomes normal
		/// </summary>
		public void Resume()
		{
			uiStore.gameObject.SetActive(false);
			Time.timeScale = 1f;
			onShop = false;
		}
		/// <summary>
		/// Pause game method, game speed becomes 0
		/// </summary>
		public void Pause()
		{
			uiStore.gameObject.SetActive(true);
			Time.timeScale = 0f;
			onShop = true;
		}
		/// <summary>
		/// Method to open inventory menu for sale
		/// </summary>
		public void OpenInventoryForSell()
		{
			uiInventory.gameObject.SetActive(true);
		}
		/// <summary>
		/// Method for returning damage that the player deals
		/// </summary>
		/// <returns>damage</returns>
		public int GetDamage()
		{
			return damage;
		}
	}
}