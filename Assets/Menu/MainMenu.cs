using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [System.Serializable]
    class Setting
    {
        //float sound;
        public float music;
    }
    public AudioMixer musicMixer;
    public Text version;
    public static bool isMusicPlay = true;
    public Toggle musicToggle;
    public Slider musicVolume;

    public static string directory = "/SaveData/";
    public static string filename = "Config.txt";
    public void Start()
    {
        Cursor.visible = true;
        Load();
        musicToggle.isOn = isMusicPlay;
        version.text = "Verion: "+Application.version;
    }
    public void Load()
    {
        string fullPath = Application.persistentDataPath + directory + filename;
        Setting s = new Setting();
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            s = JsonUtility.FromJson<Setting>(json);
            if (s.music == 0f)
                isMusicPlay = true;
            else
                isMusicPlay = false;
            musicMixer.SetFloat("musicVolume", s.music);
            musicVolume.value = s.music;
        }
        else
        {
            Debug.Log("Save file does not exist");
        }
    }
    public void OnSave()
    {
        musicMixer.GetFloat("musicVolume", out float music);
        Setting s = new Setting();
        s.music = music;
        string dir = Application.persistentDataPath + directory;
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        string json = JsonUtility.ToJson(s);
        File.WriteAllText(dir + filename, json);
        Debug.Log(json);
    }
    public void NewGame()
    {
        System.IO.File.Delete(Application.dataPath + "/player.xml");
        System.IO.File.Delete(Application.dataPath + "/inventory.xml");
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
            musicMixer.SetFloat("musicVolume", -80f);
        else
            musicMixer.SetFloat("musicVolume", 0f);
    }
    void OnEnable()
    {
        musicVolume.onValueChanged.AddListener(delegate { changeVolume(musicVolume.value); });
    }
    void changeVolume(float sliderValue)
    {
        musicMixer.SetFloat("musicVolume", sliderValue);
    }

    void OnDisable()
    {
        musicVolume.onValueChanged.RemoveAllListeners();
    }
    public void Exit()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
