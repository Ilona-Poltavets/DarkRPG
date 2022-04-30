using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class MenuPause : MonoBehaviour
{
    [SerializeField] private InputAction action = new InputAction();
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public static bool GameIsPaused = false;
    public static bool inventoryShow = false;
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

    private void goMainMenu()
    {
        pauseMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    private void Resume()
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
