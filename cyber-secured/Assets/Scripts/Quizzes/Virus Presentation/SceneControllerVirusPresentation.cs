using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControllerVirusPresentation : MonoBehaviour {

    public Button next;

	// Use this for initialization
	void Start () {

        // displays opening text
        GameObject.Find("dlg_virus_presentation").GetComponent<DialogueTrigger>().TriggerDialogue();

        // glitch animation
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
    }
	
    public void PressChoose() {
        // glitch screen
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

        // update decision text
        GameControllerV2.Instance.current_decision_text = "After studying the presentation," +
            "\nyou feel like you know how to deal with threats.";

        // deactivate quiz, and display results
        GameControllerV2.Instance.scn_virus_presentation.SetActive(false);
        GameControllerV2.Instance.DisplayDecision();
    }

}
