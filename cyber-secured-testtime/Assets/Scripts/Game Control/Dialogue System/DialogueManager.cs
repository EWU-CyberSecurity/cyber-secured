﻿/// FILE: DialogueManager.cs
/// PURPOSE: this class manages the dialogue system
/// 
/// FUNCTIONS:
/// 
/// void Start()
///     x
/// 
/// public void StartDialogue(Dialogue dialogue)
///     x
/// 
/// public void DisplayNextSentence()
///     x
/// 
/// void FixedUpdate()
///     x
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogue_box;     // textbox element to be dragged into inspector
    public GameObject portrait;         // ^
    public Text text_name;              // ^
    public Text text_dialogue;          // ^

    public int currentSentenceDisplayed = -1; // to match the index in the queue, start in -1 and increase 

    public GameObject panel;

    private ArrayList sentencesArrayList;
    private Stack<string> sentencesStack; // store the lines to make sure I can read them again

    public float elapsed_time;          // keep track of time for lerping
    public bool has_started;            // check dialogue's state
    public bool has_ended;              // ^

    public float x_pos_start_box        = 2.5f;     // the start position of each dialogue box component
    public float x_pos_start_portrait   = -6.0f;    // ^
    public float x_pos_end_box          = 16.0f;    // ^
    public float x_pos_end_portrait     = -12.0f;   // ^

    private bool proceed = true; // create a locking mechanism to prevent the user to continue when demonstration in the one time pad is going 
    private bool finished_typing = false;
    private bool pressedBackButton = false;
    private int iteration; // to not allow previous from running on 1st iteration

    // use this for initialization
    void Start()
    {
        sentencesStack = new Stack<string>(); // LIFO stracture
        sentencesArrayList = new ArrayList();
    }

    // allows for enter key to display next sentence
    void Update() 
    {
        if(!has_ended && has_started) 
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) 
            {
                DisplayNextSentenceArray(); // it was DisplayNextSentence()
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Vector2 current_pos = dialogue_box.transform.position;
        dialogue_box.transform.position = new Vector2(-16.0f, current_pos.y); // reset position for lerp
        elapsed_time = 0;
        has_started = true;
        has_ended = false;

        text_name.text = dialogue.name;

        sentencesArrayList.Clear();
        sentencesStack.Clear();

        foreach(string sentence in dialogue.sentences)
            sentencesArrayList.Add(sentence);
        
        sentencesArrayList.Reverse(); // that will flip the list to start from the first sentence
        iteration = 0;
        DisplayNextSentenceArray(); // it was DisplayNextSentence()
    }

    public void DisplayNextSentenceArray() // when the user clicks the button in the dialogue box
    {
        if (proceed) // in some cases like in the demonstration of the one time pad - the user can't proceed until demonstration is over for each sentence
        {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(3);

            // sentences haven't ended
            if (!has_ended)
            {
                // are there more sentences in the array list?
                if (sentencesArrayList.Count == 0)
                {
                    elapsed_time = 0;
                    has_ended = true;
                    has_started = false;

                    // switch out of dialogue
                    GameControllerV2.Instance.DialogueSwitch();

                    if (GameControllerV2.Instance.current_month == GameControllerV2.Instance.lastMonth)
                    {
                        GameControllerV2.Instance.ResetGame();
                    }

                    return;
                }
            }
            string sentence;
            if(pressedBackButton) // this is for the case if the back button was pressed
            {
                sentence = (string)sentencesArrayList[sentencesArrayList.Count - 1]; // give the last sentence
                sentencesArrayList.RemoveAt(sentencesArrayList.Count - 1); // remove last element from the list
                sentencesStack.Push(sentence); // to save it in the stack 
            }
            sentence = (string)sentencesArrayList[sentencesArrayList.Count - 1]; // give the last sentence
            sentencesArrayList.RemoveAt(sentencesArrayList.Count - 1); // remove last element from the list
            sentencesStack.Push(sentence); // to save it in the stack 
            pressedBackButton = false;
            iteration ++;
            
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence)); // you can disable that but you need to modify the demonstrations (10,11)
        }
    }

    // close dialogue box
    public void SkipDialogue()
    {
        elapsed_time = 0;
        has_ended = true;
        has_started = false;

        // switch out of dialogue
        GameControllerV2.Instance.DialogueSwitch();

        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(3);
    }

    public void DisplayPrecedingSentence() // when the user clicks the button in the dialoue box
    {
        if (proceed && iteration > 1) // in some cases like in the demonstration of the one time pad - the user can't proceed until demonstration is over for each sentence
        {
            // sentences haven't ended
            if (!has_ended)
            {
                // are there more sentences in the stack?
                if (sentencesStack.Count == 0)
                {
                    elapsed_time = 0;
                    has_ended = false; // was true
                    has_started = true; // was false
                    return;
                }
            }

            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(3);
            string sentence;
            if(!pressedBackButton) // first time pressing back
            {
                sentence = sentencesStack.Pop();  // gets rid of empty space
                sentencesArrayList.Add(sentence); // to read it again when the user pressing next
            }
            
            sentence = sentencesStack.Pop(); // giving the next sentence in the stack
            sentencesArrayList.Add(sentence); // to read it again when the user pressing next
            
            pressedBackButton = true;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    // looping through each character, creating a typewriter effect
    IEnumerator TypeSentence(string sentence) 
    {
        currentSentenceDisplayed++; // updating the variable to show the index of the current sentence 

        finished_typing = false;

        text_dialogue.text = "";
        foreach(char letter in sentence)
        {
            text_dialogue.text += letter;
            //yield return null; -----> If you want to type the sentence letter by letter you need to uncomment this
        }

        // check if both lengths of original sentence is the same as the sentence gets typed:
        if(text_dialogue.text.Length == sentence.Length)
        {
            finished_typing = true;
        }

        yield return null; // comment out here to type letter by letter
    }

    void FixedUpdate()
    {
        // moving the textbox smoothly
        if(has_started)
        {
            panel.GetComponent<Image>().raycastTarget = true;
            panel.GetComponent<Image>().DOColor(new Color32(50, 50, 50, 150), 0.5f);

            elapsed_time += Time.deltaTime * 4.0f;

            //dialogue box start:
            Vector2 current_pos = dialogue_box.transform.position;
            dialogue_box.transform.position = Vector2.Lerp(
                current_pos,
                new Vector2(x_pos_start_box, current_pos.y),
               0.5f * elapsed_time); 
            portrait.transform.DOMove(new Vector2(x_pos_start_portrait, current_pos.y), 0.8f);
        }

        if(has_ended)
        {
            currentSentenceDisplayed = -1; // updating the variable for the next iteration

            panel.GetComponent<Image>().raycastTarget = false;
            panel.GetComponent<Image>().DOColor(new Color32(50, 50, 50, 0), 0.5f);

            elapsed_time += Time.deltaTime;

            //dialogue box end:
            Vector2 current_pos = dialogue_box.transform.position;
            dialogue_box.transform.position = Vector2.Lerp(
                current_pos,
                new Vector2(x_pos_end_box, current_pos.y),
                0.5f * elapsed_time);
            portrait.transform.DOMove(new Vector2(x_pos_end_portrait, current_pos.y), 0.5f);
        }
    }

    public bool getProceed()
    {
        return proceed;
    }
    public void setProceed(bool x)
    {
        proceed = x;
    }

    public bool getFinished_typing() // to see if the sentence is finished to be displayed
    {
        return finished_typing;
    }
}