using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {

    public Slider AllAudioSlider;
    public Slider soundsUISlider;
    public Slider musicBackgroundSlider;
    public Slider musicQuizSlider;

    public Button muteToggle;

    public AudioMixer mixer;

    bool muted = false;

    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetQuizLevel(float sliderValue)
    {
        mixer.SetFloat("QuizVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetBackLevel(float sliderValue)
    {
        mixer.SetFloat("BackgroundVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetUILevel(float sliderValue)
    {
        mixer.SetFloat("UIVolume", Mathf.Log10(sliderValue) * 20);
    }

    private void Start()
    {
        AllAudioSlider.value = 0.5f;
        musicBackgroundSlider.value = 0.5f;
        musicQuizSlider.value = 0.5f;
        soundsUISlider.value = 0.5f;

        muteToggle.GetComponentInChildren<Text>().text = "Mute All Audio";

        //do not set this value below 0.0001f, as the unity audio mixer will break. It does not handle 0
        if (AllAudioSlider.value <= 0.0001f)
        {
            musicBackgroundSlider.value = 0.0001f;
            musicQuizSlider.value = 0.0001f;
            soundsUISlider.value = 0.0001f;
        }
    }

    //Used in editor for the clickOn in the mute toggle button
    public void Toggle()
    {
        muted = !muted;
        if (muted)
        {
            muteToggle.GetComponentInChildren<Text>().text = "Unmute all audio";
            AudioListener.volume = 0.0001f;
        }
        else
        {
            muteToggle.GetComponentInChildren<Text>().text = "Mute all audio";
            AudioListener.volume = 1.000f;
        }
    }
}
