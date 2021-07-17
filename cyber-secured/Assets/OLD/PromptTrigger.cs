//FILE: PromptTrigger.cs
//Purpose: INPUT ME
//
//
//FUNCTIONS:
//
//TriggerDialogue
//	Input ME

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class triggers dialogue
//to create new dialogue, drag this script into any GameObject and edit the dialogue in the insepector
//call the TriggerDialogue() function to trigger dialogue

public class PromptTrigger : MonoBehaviour
{
    public Prompt prompt;

    //check in insepector if dialogue is repeatable or one-time trigger;
    public bool repeatable, triggered;

    public void TriggerDialogue()
    {
        if(!triggered)
        {
            //FindObjectOfType<PromptManager>().StartDialogue(prompt);
            if(repeatable)
            {
                triggered = false;
                return;
            }
            triggered = true;
        }
    }
}
