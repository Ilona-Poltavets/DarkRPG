using UnityEngine;
using UnityEngine.InputSystem;

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

		inventory = new InventoryManager();
		uiInventory.SetInventory(inventory);
		uiInventory.gameObject.SetActive(false);

	}
	void Update()
	{
		if (Keyboard.current[Key.Space].wasPressedThisFrame)
		{
			TakeDamage(20);
		}
        if (Keyboard.current[Key.H].wasPressedThisFrame)
        {
			Healer(10);
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
			inventory.AddItem(itemWorld.GetItem());
			itemWorld.DestroySelf();
        }
    }
}
