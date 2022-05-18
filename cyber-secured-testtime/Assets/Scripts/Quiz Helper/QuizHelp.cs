﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuizHelp : MonoBehaviour
{
    int counter = 0; //setting the counter to different numbers will have it bring in different questions
    int limit = 0;
    //all the gameobjects that are used
    public GameObject cam1;
    public GameObject nameChange;
    public GameObject nameThingy;
    public GameObject gameControl;
    public GameObject diologue;
    public GameObject nextThingy;
    public GameObject sceneQuizPassword;
    public GameObject passwordContinue;
    public GameObject phishingQuizManager;
    public GameObject finalPhisher;
    public GameObject caesarQuizManager;
    public GameObject wormButton;
    public GameObject virusQuizManager;

    int run = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this will be done with a button press
        if(Input.GetKeyDown("1"))
        {
            PickQuestion(1);
            //PickQuestion(2);
            //PickQuestion(3);
            //PickQuestion(4);
            //PickQuestion(5);
        }
        if (Input.GetKeyDown("2"))
        {
            //PickQuestion(6);
            //PickQuestion(7);
            //PickQuestion(8);
            //PickQuestion(9);
            PickQuestion(10);
        }
        if (Input.GetKeyDown("3"))
        {
            //PickQuestion(11);
            //PickQuestion(12);
            PickQuestion(13);
        }
        if (Input.GetKeyDown("4"))
        {
            PickQuestion(14);
        }
        if (Input.GetKeyDown("5"))
        {
            PickQuestion(15);
        }
        if (Input.GetKeyDown("6"))
        {
            PickQuestion(16);
        }
        if (Input.GetKeyDown("7"))
        {
            PickQuestion(17);
        }
        if (Input.GetKeyDown("8"))
        {
            PickQuestion(18);
        }
        if (counter < limit)
        {
            GoToQuestion();
            counter++;
        }
    }



    public void PickQuestion(int questionNumber)
    {
        /**
         * 0-random
         * 1-password1 working
         * 2-password2 working
         * 3-password3 working
         * 4-password4 working
         * 5-password5 working
         * 6-phishing1 working
         * 7-phishing2 working
         * 8-phishing3 working
         * 9-phishing4 working
         * 10-phishing5 working
         * 11-ceasar1
         * 12-ceasar2
         * 13-ceasar3
         * 14-virus1
         * 15-virus2
         * 16-virus3
         * 17-virus4
         * 18-virus5
         * 
         * 
         * 
         * 
         * 14-encry8ption1
         * 15-encryption2
         * 16-encryption3
         * 17-malware4
         * 18-malware5
         * 19-malware6
         * 20-malware7
         */
        if (counter != limit)
            return;
        if (questionNumber == 0)
            questionNumber = Random.Range(1,11);
        switch (questionNumber)
        {
            case (1):
                counter = 0;
                limit = 8;
                break;
            case (2):
                counter = 0;
                limit = 9;
                break;
            case (3):
                counter = 0;
                limit = 10;
                break;
            case (4):
                counter = 0;
                limit = 11;
                break;
            case (5):
                counter = 0;
                limit = 12;
                break;
            case (6):
                counter = 0;
                limit = 22;
                break;
            case (7):
                counter = 0;
                limit = 23;
                break;
            case (8):
                counter = 0;
                limit = 24;
                break;
            case (9):
                counter = 0;
                limit = 25;
                break;
            case (10):
                counter = 0;
                limit = 26;
                break;
            case (11):
                counter = 0;
                limit = 37;
                break;
            case (12):
                counter = 0;
                limit = 39;
                break;
            case (13):
                counter = 0;
                limit = 41;
                break;
            case (14):
                counter = 0;
                limit = 46;
                break;
            case (15):
                counter = 0;
                limit = 47;
                break;
            case (16):
                counter = 0;
                limit = 48;
                break;
            case (17):
                counter = 0;
                limit = 49;
                break;
            case (18):
                counter = 0;
                limit = 50;
                break;
        }
    }
    public void GoToQuestion()
    {
        switch (counter)
        {
            case(0):
                nameChange.GetComponent<UnityEngine.UI.InputField>().text = "friend0";
                cam1.GetComponent<GlitchCamera>().StartGlitch();
                break;
            case (1):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;
            case (2):
                gameControl.GetComponent<GameControllerV2>().SetCompany(1);
                break;
            case (3):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (4):
                gameControl.GetComponent<GameControllerV2>().EventYesNo(true);
                break;
            case (5):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;
            case (6):
                nextThingy.GetComponent<SceneControllerPasswordIntro>().ContinueOnClick();
                break;
            case (7):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break; ////////password 1
            case (8):
                sceneQuizPassword.GetComponent<PasswordSceneController>().onMultiAnswerButton1Clicked();
                sceneQuizPassword.GetComponent<PasswordSceneController>().nextQuestion();
                break; ////////password 2
            case (9):
                sceneQuizPassword.GetComponent<PasswordSceneController>().onMultiAnswerButton1Clicked();
                sceneQuizPassword.GetComponent<PasswordSceneController>().nextQuestion();
                break; ////////password 3
            case (10):
                sceneQuizPassword.GetComponent<PasswordSceneController>().onMultiAnswerButton1Clicked();
                sceneQuizPassword.GetComponent<PasswordSceneController>().nextQuestion();
                break;/////////password 4
            case (11):
                sceneQuizPassword.GetComponent<PasswordSceneController>().onMultiAnswerButton1Clicked();
                sceneQuizPassword.GetComponent<PasswordSceneController>().nextQuestion();
                break;/////////password 5
            case (12):
                sceneQuizPassword.GetComponent<PasswordSceneController>().onMultiAnswerButton1Clicked();
                sceneQuizPassword.GetComponent<PasswordSceneController>().nextQuestion();
                break;
            case (13):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (14):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;
            case (15):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (16):
                gameControl.GetComponent<GameControllerV2>().EventYesNo(false);
                break;
            case (17):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (18):
                gameControl.GetComponent<GameControllerV2>().EventYesNo(false);
                break;
            case (19):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (20):
                gameControl.GetComponent<GameControllerV2>().EventYesNo(true);
                break;
            case (21):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break; //////// phishing1 6
            case (22):
                phishingQuizManager.GetComponent<PhishingQuizManager>().nextSet();
                break; ////////phishing2 7
            case (23):
                phishingQuizManager.GetComponent<PhishingQuizManager>().nextSet();
                break; ////////phishing3 8
            case (24):
                phishingQuizManager.GetComponent<PhishingQuizManager>().nextSet();
                break; ////////phishing4 9
            case (25):
                phishingQuizManager.GetComponent<PhishingQuizManager>().nextSet();
                break; ////////phishing5 10
            case (26):
                finalPhisher.GetComponent<ChoiceManager>().message();
                finalPhisher.GetComponent<FinalPhishingDialogue>().DisplayDialogue(true);
                break;
            case (27):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;
            case (28):
                phishingQuizManager.GetComponent<PhishingQuizManager>().nextSet();
                break;
            case (29):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (30):
                gameControl.GetComponent<GameControllerV2>().EventYesNo(false);
                break;
            case (31):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (32):
                gameControl.GetComponent<GameControllerV2>().EventYesNo(false);
                break;
            case (33):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;
            case (34):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (35):
                gameControl.GetComponent<GameControllerV2>().EventYesNo(true);
                break;
            case (36):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;///ceasar1 11
            case (37):
                caesarQuizManager.GetComponent<CaesarQuizManager>().NextSet();
                break;
            case (38):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;//ceasar2 12
            case (39):
                caesarQuizManager.GetComponent<CaesarQuizManager>().NextSet();
                break;
            case (40):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;//ceasar3 13
            case (41):
                caesarQuizManager.GetComponent<CaesarQuizManager>().NextSet();
                break;
            case (42):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;
            case (43):
                gameControl.GetComponent<GameControllerV2>().HideDecision();
                break;
            case (44):
                gameControl.GetComponent<GameControllerV2>().EventYesNo(true);
                break;
            case (45):
                diologue.GetComponent<DialogueManager>().SkipDialogue();
                break;//malware1 14
            case (46):
                virusQuizManager.GetComponent<VirusQuizManager>().nextSet();
                break;//malware2 15
            case (47):
                virusQuizManager.GetComponent<VirusQuizManager>().nextSet();
                break;//malware3 16
            case (48):
                virusQuizManager.GetComponent<VirusQuizManager>().nextSet();
                break;//malware4 17
            case (49):
                virusQuizManager.GetComponent<VirusQuizManager>().nextSet();
                break;//malware4 18
        }
    }
}