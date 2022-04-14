using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider volumeControl;
    private AudioSource snd;

    void Start()
    {
        volumeControl.onValueChanged.AddListener(delegate { VolumeChanged(); });
        snd = GetComponent<AudioSource>();
        snd.volume = GameData.volume;
        snd.Play();
        volumeControl.value = GameData.volume;
    }
    void VolumeChanged() 
    { 
     GameData.volume = volumeControl.value;
     snd.volume = GameData.volume;
    }
}
