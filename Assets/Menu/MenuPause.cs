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
    public static bool inventoryShow = false;
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
            else
            {
                Pause();
            }
        }
        if (Keyboard.current[Key.I].wasPressedThisFrame)
        {
            if (inventoryShow)
            {
                CloseInventory();
                inventoryShow = false;
            }
            else
            {
                ShowInventory();
                inventoryShow = true;
            }
        }
    }
    private void CloseInventory()
    {
        inventoryUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ShowInventory()
    {
        inventoryUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void goMainMenu()
    {
        pauseMenuUI.SetActive(false);
        Serializator.SaveXml(player.characteristics, Application.dataPath + "/player.xml");
        Serializator.SaveInventory(inventoryUI.GetComponent<UI_inventory>().GetInventrory()/*.GetItemList()*/, Application.dataPath + "/inventory.xml");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void savegame()
    {
        Serializator.SaveXml(player.characteristics, Application.dataPath + "/player.xml");
        Serializator.SaveInventory(inventoryUI.GetComponent<UI_inventory>().GetInventrory()/*.GetItemList()*/, Application.dataPath + "/inventory.xml");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Shop");
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
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
