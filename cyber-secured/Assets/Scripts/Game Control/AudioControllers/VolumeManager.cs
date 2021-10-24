using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {
    public Slider musicSlider;
    public MuteToggle muteToggle;

    private void Start()
    {
        musicSlider.value = 0.5f;
        muteToggle = GameObject.Find("AudioToggle").GetComponent<MuteToggle>();
    }

    public void MusicSlider() {
        muteToggle = GameObject.Find("AudioToggle").GetComponent<MuteToggle>();
        if (!muteToggle.getMuted())
        {
            AudioListener.volume = musicSlider.value;
        }
    }
}
