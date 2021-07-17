//FILE: PromptManager.cs
//Purpose: INPUT ME
//
//
//FUNCTIONS:
//
//Start
//	INPUT ME
//
//DisplayNextSentence
//	INPUT ME
//
//FixedUpdate
//	INPUT ME
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class manages the dialogue system

public class PromptManager : MonoBehaviour
{
    public GameObject dialogue_box;     //textbox element to be dragged into inspector
    public GameObject portrait;         //^
    public Text text_name;              //^
    public Text text_dialogue;          //^

    private Queue<string> sentences;    //store lines of dialogue

    public float elapsed_time;          //keep track of time for lerping
    public bool has_started;            //check dialogue's state
    public bool has_ended;              //^

    public float x_pos_start_box        = 2.5f;     //the start position of each dialogue box component
    public float x_pos_start_portrait   = -6.0f;    //^
    public float x_pos_end_box          = 16.0f;    //^
    public float x_pos_end_portrait     = -12.0f;   //^

	// Use this for initialization
	void Start()
	{
        sentences = new Queue<string>(); //FIFO system
	}

    //TODO: more comments

    /*public void StartDialogue(Dialogue dialogue)
    {
        Vector2 current_pos = dialogue_box.transform.position;
        dialogue_box.transform.position = new Vector2(-16.0f, current_pos.y);    //reset position for lerp
        elapsed_time = 0;
        has_started = true;
        has_ended = false;

        text_name.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }*/

    public void DisplayNextSentence()
    {
        //are there more sentences in the queue?
        if(sentences.Count == 0)
        {
            //FindObjectOfType<AudioController>().SoundStopTalking(); //stop talking audio
            //EndDialogue();
            elapsed_time = 0;
            has_ended = true;
            has_started = false;
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //looping through each character, creating a typewriter effect
    IEnumerator TypeSentence(string sentence)
    {
        //FindObjectOfType<AudioController>().SoundStartTalking();    //start talking audio
        text_dialogue.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            text_dialogue.text += letter;
            yield return null;
        }
    }

    /*
    void EndDialogue()
    {
        //Debug.Log("End of conversation.");
    }*/

    void FixedUpdate()
    {
        //TODO: too much repetition, encapsulate or something like that
        //moving the textbox smoothly
        if(has_started)
        {
            elapsed_time += Time.deltaTime;

            //dialogue box start:
            Vector2 current_pos = dialogue_box.transform.position;
            dialogue_box.transform.position = Vector2.Lerp(
                current_pos,
                new Vector2(x_pos_start_box, current_pos.y),
                0.5f * elapsed_time);

            //portrait start:
            Vector2 current_pos_portait = portrait.transform.position;
            portrait.transform.position = Vector2.Lerp(
                current_pos_portait,
                new Vector2(x_pos_start_portrait, current_pos_portait.y),
                0.5f * elapsed_time);
        }

        if(has_ended)
        {
            elapsed_time += Time.deltaTime;

            //dialogue box end:
            Vector2 current_pos = dialogue_box.transform.position;
            dialogue_box.transform.position = Vector2.Lerp(
                current_pos,
                new Vector2(x_pos_end_box, current_pos.y),
                0.5f * elapsed_time);

            //portrait end:
            Vector2 current_pos_portait = portrait.transform.position;
            portrait.transform.position = Vector2.Lerp(
                current_pos_portait,
                new Vector2(x_pos_end_portrait, current_pos_portait.y),
                0.5f * elapsed_time);
        }
    }
}
