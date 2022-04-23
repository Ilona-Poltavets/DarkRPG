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

	//public Text hp;
	//public Text damagePoints;
	//public Text defencePoints;

	private InventoryManager inventory;
	[SerializeField] private UI_inventory uiInventory;
    private void Awake()
    {
		maxHealth = 100;
		exp = 10;
		lvl = 1;
		defense = 20;
		damage = 10;

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

		//hp.text = currentHealth.ToString();
		//defencePoints.text = defense.ToString();
		//damagePoints.text = damage.ToString();
	}
	private void UseItem(Item item)
    {
        switch (item.itemType)
        {
			case Item.ItemType.HealthPotion:
				Healer(10);
				inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
				break;
			//case Item.ItemType.ManaPotion:
			//	inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
			//	break;
			case Item.ItemType.Medkit:
				Healer(30);
				inventory.RemoveItem(new Item { itemType = Item.ItemType.Medkit, amount = 1 });
				break;
            default:
				uiInventory.SetEquipment(item);
				break;
        }
    }
	void Update()
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
		float k = maxHealth * 0.45f;
		maxHealth += 100 + Mathf.RoundToInt(k);
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		expBar.SetExp(exp);
		expBar.SetLevel(lvl);
    }
    private void OnTriggerEnter(Collider other)
    {
		ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
			if (inventory.itemList.Count < 56)
			{
				inventory.AddItem(itemWorld.GetItem());
				itemWorld.DestroySelf();
			}
        }
    }
}
