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
    }

    public void MusicSlider() {
        if (muteToggle.getMuted() == false)
        {
            AudioListener.volume = musicSlider.value;
        }
    }
}
