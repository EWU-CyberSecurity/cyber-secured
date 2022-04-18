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

    private float previous_value_master = .5f;
    private float previous_value_ui = .5f;
    private float previous_value_background = .5f;
    private float previous_value_quiz = .5f;

    public void SetMasterLevel(float sliderValue)
    {
        Debug.Log("setting master level: " + muted + " ");
        if (AudioListener.volume == 0.0001f)
        {
            muted = false;
            AudioListener.volume = 1.000f;
        }
        muteToggle.GetComponentInChildren<Text>().text = "Mute all audio";
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetQuizLevel(float sliderValue)
    {
        if (AudioListener.volume == 0.0001f)
        {
            muted = false;
            AudioListener.volume = 1.000f;
        }
        muteToggle.GetComponentInChildren<Text>().text = "Mute all audio";
        mixer.SetFloat("QuizVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetBackLevel(float sliderValue)
    {
        if (AudioListener.volume == 0.0001f)
        {
            muted = false;
            AudioListener.volume = 1.000f;
        }
        muteToggle.GetComponentInChildren<Text>().text = "Mute all audio";
        mixer.SetFloat("BackgroundVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetUILevel(float sliderValue)
    {
        if (AudioListener.volume == 0.0001f)
        {
            muted = false;
            AudioListener.volume = 1.000f;
        }
        muteToggle.GetComponentInChildren<Text>().text = "Mute all audio";
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

    // Used in editor for the clickOn in the mute toggle button
    // This is a pretty hacky way to do this and it could be improved.
    public void Toggle()
    {
        Debug.Log("volume: " + AudioListener.volume + " muted? " + muted);
        muted = !muted;
        if (muted)
        {
            Slider slider = GameObject.Find("Master Volume Slider").GetComponent<Slider>();
            previous_value_master = slider.value;
            slider.value = slider.minValue;

            slider = GameObject.Find("UI Volume Slider").GetComponent<Slider>();
            previous_value_ui = slider.value;
            slider.value = slider.minValue;

            slider = GameObject.Find("Background Music Volume Slider").GetComponent<Slider>();
            previous_value_background = slider.value;
            slider.value = slider.minValue;

            slider = GameObject.Find("Quiz Music Volume Slider").GetComponent<Slider>();
            previous_value_quiz = slider.value;
            slider.value = slider.minValue;

            muteToggle.GetComponentInChildren<Text>().text = "Unmute all audio";
            AudioListener.volume = 0.0001f;
        }
        else
        {

            Slider slider = GameObject.Find("Master Volume Slider").GetComponent<Slider>();
            slider.value = previous_value_master;

            slider = GameObject.Find("UI Volume Slider").GetComponent<Slider>();
            slider.value = previous_value_ui;

            slider = GameObject.Find("Background Music Volume Slider").GetComponent<Slider>();
            slider.value = previous_value_background;

            slider = GameObject.Find("Quiz Music Volume Slider").GetComponent<Slider>();
            slider.value = previous_value_quiz;

            muteToggle.GetComponentInChildren<Text>().text = "Mute all audio";
            AudioListener.volume = 1.000f;
        }

    }
}
