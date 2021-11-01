using UnityEngine;

public class FinalPhishingDialogue : MonoBehaviour {

    public void DisplayDialogue(bool correct) {
        if(correct) {
            GameObject.Find("ChaseCorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
        } else {
            GameObject.Find("ChaseIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
