using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Player : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;
	public Rigidbody rb;

	private InventoryManager inventory;
	[SerializeField] private UI_inventory uiInventory;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		inventory = new InventoryManager(UseItem);
		uiInventory.SetPlayer(this);
		uiInventory.SetInventory(inventory);
		uiInventory.gameObject.SetActive(false);

	}
	private void UseItem(Item item)
    {
        switch (item.itemType)
        {
			case Item.ItemType.HealthPotion:
				Healer(10);
				inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
				break;
			case Item.ItemType.ManaPotion:
				inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
				break;

        }
    }
	void Update()
	{
		if (Keyboard.current[Key.Space].wasPressedThisFrame)
		{
			TakeDamage(20);
		}
	}
	void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);
	}
	void Healer(int points)
    {
		currentHealth += points;
		healthBar.SetHealth(currentHealth);
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
