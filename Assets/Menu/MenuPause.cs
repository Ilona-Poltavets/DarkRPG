using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private InputAction action = new InputAction();
    [SerializeField] private Player player;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public static bool GameIsPaused = false;
    //public static bool inventoryShow = false;
    [SerializeField] private GameObject shopUI;
    //[SerializeField] private UI_inventory uiInventory;
    // Update is called once per frame

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    void Update()
    {
        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else if (!Player.onShop)
            {
                Pause();
            }
        }
        if (Keyboard.current[Key.I].wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                CloseInventory();
            }
            else
            {
                ShowInventory();
            }
        }
    }
    private void CloseInventory()
    {
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void ShowInventory()
    {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public static void GamePause()
    {
        GameIsPaused = !GameIsPaused;
    }
    public void goMainMenu()
    {
        pauseMenuUI.SetActive(false);
        Serializator.SaveXml(player.characteristics, Application.dataPath + "/player.xml");
        Serializator.SaveInventory(inventoryUI.GetComponent<UI_inventory>().GetInventrory().GetItemList(), Application.dataPath + "/inventory.xml");
        Serializator.SaveEquipment(inventoryUI.GetComponent<UI_inventory>().GetInventrory().GetEquipment(), Application.dataPath + "/equip.xml");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void savegame()
    {
        Serializator.SaveXml(player.characteristics, Application.dataPath + "/player.xml");
        Serializator.SaveInventory(inventoryUI.GetComponent<UI_inventory>().GetInventrory().GetItemList(), Application.dataPath + "/inventory.xml");
        Serializator.SaveEquipment(inventoryUI.GetComponent<UI_inventory>().GetInventrory().GetEquipment(), Application.dataPath + "/equip.xml");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Shop");
    }
    public void Resume()
    {
        if(pauseMenuUI.active)
            pauseMenuUI.SetActive(false);
        if(inventoryUI.active)
            inventoryUI.SetActive(false);
        if(shopUI.active)
            shopUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
