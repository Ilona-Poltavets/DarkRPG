using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public static bool GameIsPaused = false;
    public static bool inventoryShow = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        if (Input.GetKeyDown(KeyCode.I))
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
    public void CloseInventory()
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
        SceneManager.LoadScene("MainMenu");
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
