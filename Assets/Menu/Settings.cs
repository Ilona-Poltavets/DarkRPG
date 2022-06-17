using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
[XmlRoot("Settings")]
public class Settings
{
    [XmlElement("musicVolume")]
    public float musicVolume { get; set; }
    [XmlElement("soundVolume")]
    public float soundVolume { get; set; }
    public Settings()
    {
        musicVolume = 0f;
        soundVolume = 0f;
    }
    public Settings(float musicVolume,float soundVolume)
    {
        this.musicVolume = musicVolume;
        this.soundVolume = soundVolume;
    }
}
