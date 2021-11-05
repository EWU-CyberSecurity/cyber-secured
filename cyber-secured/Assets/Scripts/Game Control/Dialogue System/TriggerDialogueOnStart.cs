using UnityEngine;

//this script just triggers the dialogue upon scene load
public class TriggerDialogueOnStart : MonoBehaviour
{
    void FixedUpdate()
    {
        this.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        Destroy(this);
    }
}
