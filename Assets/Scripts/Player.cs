using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using UnityEngine.UI;
using System.Timers;
using System.Collections;
public class Player : MonoBehaviour
{
	[SerializeField] private HealthBar healthBar;
	[SerializeField] private ExpBar expBar;
	private Rigidbody rb;
	private Animator animator;

	//Characteristics
	public Characteristics characteristics;    // отражающий класс
	public int currentHealth;
	private string datapath;

	private InventoryManager inventory;
	[SerializeField] private UI_inventory uiInventory;
	[SerializeField] private StoreItem uiStore;
	[SerializeField] private Text textLvlUp;
	[SerializeField] private GameObject dieText;
	public static bool onShop = false;
	private void Awake()
	{
		Cursor.visible = false;
		datapath = Application.dataPath + "/player.xml";

		if (System.IO.File.Exists(datapath))
			characteristics = Serializator.DeXml(datapath);
		else
			setDefault();
		if (System.IO.File.Exists(Application.dataPath + "/inventory.xml"))
		{
			Debug.Log("File is founded");
			inventory = Serializator.GetInventory(Application.dataPath + "/inventory.xml");
			inventory.useItemAction = UseItem;
		}
		else
		{
			Debug.Log("File isn't founded");
			inventory = new InventoryManager(UseItem);
		}
	}
	void setDefault()
	{
		characteristics = new Characteristics();
	}
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		currentHealth = characteristics.maxHealth;
		Debug.Log(currentHealth);
		healthBar.SetMaxHealth(characteristics.maxHealth);

		expBar.SetLevel(characteristics.lvl);
		expBar.SetExp(characteristics.exp);

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
		if (characteristics.exp > 1000*characteristics.lvl)
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
		currentHealth -= (damage - characteristics.defense);
		healthBar.SetHealth(currentHealth);
	}
	void Healer(int points)
	{
		currentHealth += points;
		healthBar.SetHealth(currentHealth);
	}
	public void AddExp(int points)
	{
		characteristics.exp += points;
		expBar.SetExp(characteristics.exp);
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
			else if (rand <= 2)
				animator.SetTrigger("levelUp2");
			else if (rand <= 3)
				animator.SetTrigger("levelUp3");
			else if (rand <= 4)
				animator.SetTrigger("levelUp4");
			else if (rand <= 5)
				animator.SetTrigger("levelUp5");
		}
		characteristics.lvl += 1;
		characteristics.exp -= 1000;
		float k1 = characteristics.maxHealth * 0.45f;
		characteristics.maxHealth += 100 + Mathf.RoundToInt(k1);
		currentHealth = characteristics.maxHealth;
		characteristics.defense += 10;
		characteristics.damage += 12;
		healthBar.SetMaxHealth(characteristics.maxHealth);
		expBar.SetExp(characteristics.exp);
		expBar.SetLevel(characteristics.lvl);
	}
	private void OnTriggerEnter(Collider other)
	{
		ItemWorld itemWorld = other.GetComponent<ItemWorld>();
		if (itemWorld != null)
		{
			if (itemWorld.GetItem().itemType == Item.ItemType.Gold)
			{
				characteristics.gold += itemWorld.GetItem().amount;
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
		return characteristics.damage;
	}
}
