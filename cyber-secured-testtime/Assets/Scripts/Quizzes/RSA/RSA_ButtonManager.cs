﻿using UnityEngine;
using UnityEngine.UI;

public class RSA_ButtonManager : MonoBehaviour {

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

            GameObject.Find("RSA_questions").GetComponent<RSA_SceneControllerQuestions>().DecreaseLife();
            text.text = "That's incorrect!";
        }

        disable = true;
    }

}