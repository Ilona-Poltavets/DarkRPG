using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using UnityEngine.UI;
using System.Timers;
using System.Collections;

public class Player : MonoBehaviour
{
	private static Timer aTimer;

	[SerializeField] private HealthBar healthBar;
	[SerializeField] private ExpBar expBar;
	private Rigidbody rb;
	private Animator animator;

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
	[SerializeField] private Text textLvlUp;
	[SerializeField] private GameObject dieText;
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
		rb = GetComponent<Rigidbody>();
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
	public void TakeDamage(int damage)
	{
		currentHealth -= (damage-defense);
		healthBar.SetHealth(currentHealth);
	}
	void Healer(int points)
    {
		currentHealth += points;
		healthBar.SetHealth(currentHealth);
    }
	public void AddExp(int points)
    {
		exp += points;
		expBar.SetExp(exp);
		//Debug.Log(points);
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
			else if(rand <= 2)
				animator.SetTrigger("levelUp2");
			else if(rand <= 3)
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
	public int GetDamage()
    {
		return damage;
    }
}
