using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public HealthBar healthBar;
	public ExpBar expBar;
	public Rigidbody rb;

	//Characteristics
	public int maxHealth;
	public int currentHealth;
	public int lvl = 1;
	public int exp;
	public int defense;
	public int damage;
	public int gold;

	private InventoryManager inventory;
	[SerializeField] private UI_inventory uiInventory;
	[SerializeField] private StoreItem uiStore;
	public static bool onShop = false;
    private void Awake()
    {
		maxHealth = 100;
		exp = 10;
		lvl = 1;
		defense = 10;
		damage = 10;
		gold = 1000;
	}
    void Start()
	{
		rb = GetComponent<Rigidbody>();
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		expBar.SetLevel(lvl);
		expBar.SetExp(exp);

		inventory = new InventoryManager(UseItem);
		uiInventory.SetPlayer(this);
		uiInventory.SetInventory(inventory);
		uiInventory.gameObject.SetActive(false);
		uiStore.SetPlayer(this);
		uiStore.SetInventory(inventory);
		uiStore.gameObject.SetActive(false);
	}
	private void UseItem(Item item)
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
				break;
        }
    }
	void FixedUpdate()
	{
		if (Keyboard.current[Key.Space].wasPressedThisFrame)
		{
			AddExp(500);
		}
        if (Keyboard.current[Key.D].wasPressedThisFrame)
        {
			TakeDamage(50);
        }
        if (exp > 1000)
        {
			LevelUp();
        }
        if (Keyboard.current[Key.G].wasPressedThisFrame)
        {
			ItemWorld.DropItem(rb.position, ItemWorldSpawner.GenerateGold());
        }
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            if (onShop)
            {
                Resume();
            }
        }
    }
	void TakeDamage(int damage)
	{
		currentHealth -= (damage-defense);
		healthBar.SetHealth(currentHealth);
	}
	void Healer(int points)
    {
		currentHealth += points;
		healthBar.SetHealth(currentHealth);
    }
	void AddExp(int points)
    {
		exp += points;
		expBar.SetExp(exp);
    }
	private void LevelUp()
    {
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
	public void Resume()
	{
		uiStore.gameObject.SetActive(false);
		Time.timeScale = 1f;
		onShop = false;
	}
	void Pause()
	{
		uiStore.gameObject.SetActive(true);
		Time.timeScale = 0f;
		onShop = true;
	}
	public void OpenInventoryForSell()
	{
		uiInventory.gameObject.SetActive(true);
	}
}
