﻿using UnityEngine;

public class SceneControllerPasswordIntro : MonoBehaviour {

    public GameObject buttons;
    public GameObject question_box;
    public GameObject continue_button;
    public GameObject hint_button;
    public GameObject password_intro;
    public GameObject quiz_start_dialogue;
    public GameObject scene_controller;

    // Use this for initialization
    void Start () {

    }
    
    public void ContinueOnClick()
    {
        // glitch screen
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
        scene_controller.GetComponent<PasswordSceneController>().nextQuestion();
        AudioControllerV2 audioController = GameObject.Find("SoundManager").GetComponent<AudioControllerV2>();

        // Set these to be active first so that the player can't see
        // them being shuffled. 
        buttons.SetActive(true);
        question_box.SetActive(true);
        hint_button.SetActive(true);
        quiz_start_dialogue.SetActive(true);

        password_intro.SetActive(false);

        audioController.PlayQuizMusic();
        //quiz_start_dialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    //team b addition
}