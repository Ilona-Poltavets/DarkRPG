using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public Text version;
    public static bool isMusicPlay = true;
    public void Start()
    {
        Debug.Log("Start!");
        version.text = "Verion: "+Application.version;
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Continue()
    {
        Debug.Log("Continue");
    }
    public void OnOffMusic()
    {
        isMusicPlay = !isMusicPlay;
        if (!isMusicPlay)
            musicMixer.SetFloat("musicVolume", -80f);
        else
            musicMixer.SetFloat("musicVolume", 0f);
    }
    public void Exit()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
