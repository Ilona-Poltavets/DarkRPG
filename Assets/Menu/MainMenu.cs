using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer musicMixer;

    public Text version;
    private Settings settings;

    public Toggle musicToggle;
    public Slider musicVolume;

    public Toggle soundToggle;
    public Slider soundVolume;

    public static bool isMusicPlay = true;
    public static string datapath = "/settings.xml";
    public void Start()
    {
        Cursor.visible = true;
        Load();
        musicToggle.isOn = isMusicPlay;
        version.text = "Verion: "+Application.version;
    }
    public void Load()
    {
        if (File.Exists(Application.dataPath + datapath))
        {
            Debug.Log("File is found");
            settings = Serializator.GetSettings(Application.dataPath + datapath);
            Debug.Log(settings.musicVolume);
            Debug.Log(settings.soundVolume);
            musicMixer.SetFloat("musicVolume", settings.musicVolume);
            musicMixer.SetFloat("soundVolume", settings.soundVolume);
            musicVolume.value = settings.musicVolume;
            soundVolume.value = settings.soundVolume;
        }
        else
        {
            settings = new Settings();
        }
        
        musicVolume.value = settings.musicVolume;
        soundVolume.value = settings.soundVolume;
    }
    void Update()
    {
        if (settings.musicVolume == -80f)
        {
            musicToggle.isOn = false;
        }
        if (settings.soundVolume == -80f)
        {
            soundToggle.isOn = false;
        }
    }
    public void OnSave()
    {
        Serializator.SaveSettings(settings, Application.dataPath + datapath);
    }
    public void NewGame()
    {
        File.Delete(Application.dataPath + "/player.xml");
        File.Delete(Application.dataPath + "/inventory.xml");
        SceneManager.LoadScene("Demo Blue");
    }
    public void Continue()
    {
        SceneManager.LoadScene("Demo Blue");
    }
    public void OnOffMusic()
    {
        isMusicPlay = !isMusicPlay;
        if (!isMusicPlay)
        {
            musicMixer.SetFloat("musicVolume", -80f);
            settings.musicVolume = -80f;
        }
        else
        {
            musicMixer.SetFloat("musicVolume", 0f);
            settings.musicVolume = 0f;
        }
    }
    public void OnOffSound()
    {
        isMusicPlay = !isMusicPlay;
        if (!isMusicPlay)
        {
            musicMixer.SetFloat("soundVolume", -80f);
            settings.soundVolume = -80f;
        }
        else
        {
            musicMixer.SetFloat("soundVolume", 0f);
            settings.soundVolume = 0f;
        }
    }
    void OnEnable()
    {
        musicVolume.onValueChanged.AddListener(delegate { changeMusicVolume(musicVolume.value); });
        soundVolume.onValueChanged.AddListener(delegate { changeSoundVolume(soundVolume.value); });
    }
    void changeMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("musicVolume", sliderValue);
        settings.musicVolume = sliderValue;
    }
    void changeSoundVolume(float sliderValue)
    {
        musicMixer.SetFloat("soundVolume", sliderValue);
        settings.soundVolume = sliderValue;
    }
    void OnDisable()
    {
        musicVolume.onValueChanged.RemoveAllListeners();
        soundVolume.onValueChanged.RemoveAllListeners();
    }
    public void Exit()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
