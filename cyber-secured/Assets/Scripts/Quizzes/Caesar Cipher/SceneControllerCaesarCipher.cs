using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerCaesarCipher : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

        // displays opening text
        GameObject.Find("dlg_caesar").GetComponent<DialogueTrigger>().TriggerDialogue();

        // glitch animation
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
    }
    
}
