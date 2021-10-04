/// FILE: DialogueTrigger.cs
/// PURPOSE: this class triggers dialogue
/// 
/// to create new dialogue, drag this script into any GameObject and edit the dialogue in the insepector
/// call the TriggerDialogue() function to trigger dialogue
/// 
/// FUNCTIONS:
/// 
/// public void TriggerDialogue()
///     triggers dialogue if not triggered already, using DialogueManager to control output
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //check in insepector if dialogue is repeatable or one-time trigger;
    public bool repeatable, triggered = false;

    public GameObject picture; // the IT logo

    private void Start()
    {
        picture = GameObject.FindWithTag("IT_picture");

    }
    private void FixedUpdate()
    {
        // why rotation though?
        picture.transform.Rotate(0, 0.05f , 0);
    }

    public void TriggerDialogue()
    {
        // don't start another piece of dialogue if still in one
        if(!GameControllerV2.Instance.InDialogue())
        {
            if(!triggered)
            {
                // switch to in_dialogue
                GameControllerV2.Instance.DialogueSwitch();

                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

                if(repeatable)
                {
                    triggered = false;
                    return;
                }
                triggered = true;
            }
        }
    }
}
