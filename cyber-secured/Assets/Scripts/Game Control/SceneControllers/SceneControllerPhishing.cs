using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class SceneControllerPhishing : MonoBehaviour
{
    public GameObject wave;                 // --

    public Button btn_link_1;               // --
    public Button btn_link_2;               // --
    public Button btn_link_3;               // --

    public Text btn_1_text;                 // --
    public Text btn_2_text;                 // --
    public Text btn_3_text;                 // --

    public Button btn_spam;                 // --
    public Button btn_not_spam;             // --

    public GameObject pnl_image_spam;       // background panel for spam
    public Image[] images;                  // the image bank
    public Image img_spam;                  // the actual image
    
    public int correct_button;              // --

    public int current_round;               // --

    public string[] choice = new string[3]; // --

    void Start()
    {
        // it's wavy baby, silver surfer - makes the wave go up and down repeatedly
        wave.transform.DOLocalMoveY(0, 1).SetLoops(-1, LoopType.Yoyo);

        // initialize current round tracker
        current_round = 1;

        // set the choices for the round, and maybe play dialogue
        SetChoices(current_round);

        // randomize the answer
        RandomizeAnswers();
    }

    public void SetChoices(int current_round)
    {
        // TODO: play some dialogue

        // choice[0] should always be the correct answer.
        switch(current_round)
        {
            case(1):
            {
                choice[0] = "https://www.google.com";
                choice[1] = "https://www.g00gle.com";
                choice[2] = "https://www.goog1e.com";
            } break;

            case(2):
            {
                choice[0] = "https://www.youtube.com";
                choice[1] = "https://www.you.tube.com";
                choice[2] = "https://www.youtub3.com";
            } break;

            case(3):
            {
                choice[0] = "https://www.facebook.com";
                choice[1] = "https://www.facebo.ok.com";
                choice[2] = "https://www.face.book.com";
            } break;
        }
    }

    public void RandomizeAnswers()
    {
        // set the correct button to a number between 1 and 3
        correct_button = Random.Range(1, 4);

        switch(correct_button)
        {
            case(1):
            {
                btn_1_text.text = choice[0];
                btn_2_text.text = choice[1];
                btn_3_text.text = choice[2];
            } break;

            case(2):
            {
                btn_1_text.text = choice[1];
                btn_2_text.text = choice[0];
                btn_3_text.text = choice[2];
            } break;

            case(3):
            {
                btn_1_text.text = choice[2];
                btn_2_text.text = choice[1];
                btn_3_text.text = choice[0];
            } break;
        }
    }

    public void SpamButton(int x)
    {
        if(x == 1) // if "Not Spam" is pressed
        {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);
        } else {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);
        }
            
        // end of second part
        if(current_round == 7)
        {
            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // update decision text
            GameControllerV2.Instance.current_decision_text = "You passed! (event still a work-in-progress)";

            // deactivate quiz, and display results
            GameControllerV2.Instance.scn_phishing_v2.SetActive(false);
            GameControllerV2.Instance.DisplayDecision();
        }

        current_round++;

        img_spam.sprite = images[current_round - 3].sprite;
    }

    public void ChooseButton(int x)
    {
        if(x == correct_button)
        {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

            Debug.Log("You are correct!");
        } else {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);

            Debug.Log("You are wrong!");
        }

        current_round++;

        // end of first part
        if(current_round == 4)
        {
            // deactive the first three buttons
            btn_link_1.gameObject.SetActive(false);
            btn_link_2.gameObject.SetActive(false);
            btn_link_3.gameObject.SetActive(false);

            btn_spam.gameObject.SetActive(true);
            btn_not_spam.gameObject.SetActive(true);
            pnl_image_spam.SetActive(true);
        }

        SetChoices(current_round);

        RandomizeAnswers();
    }
}
