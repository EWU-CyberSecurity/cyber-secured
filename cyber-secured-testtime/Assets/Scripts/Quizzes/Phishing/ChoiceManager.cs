﻿using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour {

    public Text text;

    public bool correct;
    public bool disable;

    public void message() {
        if (correct) {

            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

            text.text = "That's correct!";

            // increase NP by 2
            GameControllerV2.Instance.IncreaseNP(2);

        } else {

            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);

            GameObject.Find("scn_phishing_v2").GetComponent<SceneControllerPhishingV2>().DecreaseLife();
            text.text = "That's incorrect!";
        }

        disable = true;
    }
    public void ResetWithAnswer(bool correct)
    {
        if (correct)
        {
            PlayerPrefs.SetInt("QuestionRight", 1);
        }
        else
        {
            PlayerPrefs.SetInt("QuestionRight", 0);
        }
        GameObject gameControl = GameObject.Find("GameControllerV2"); 
        gameControl.GetComponent<GameControllerV2>().ResetGame();
    }
}