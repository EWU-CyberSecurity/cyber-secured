using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupDialogue : MonoBehaviour {

	public void DisplayDialogue(int num) {
        switch (num) {
        case 1: GameObject.Find("HDD_Dialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
            break;
        case 2: GameObject.Find("USB_Dialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
            break;
        case 3: GameObject.Find("CLOUD_Dialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
            break;
        default:
            Debug.Log("");
            break;
        }
        
    }
}

