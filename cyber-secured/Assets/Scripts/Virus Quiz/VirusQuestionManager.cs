using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VirusQuestionManager : MonoBehaviour {
    public Button aButton;
    public Button bButton;
    public Button cButton;
    public Button dButton;

    public GameObject next;

    // Update is called once per frame
    void Update() {
        
        if (aButton.GetComponent<VirusChoiceManager>().disable == true ||
            bButton.GetComponent<VirusChoiceManager>().disable == true ||
            cButton.GetComponent<VirusChoiceManager>().disable == true ||
            dButton.GetComponent<VirusChoiceManager>().disable == true) 
        {
            disableButtons();
            next.SetActive(true);
        }
    }

    void disableButtons() {
        aButton.GetComponent<Button>().interactable = false;
        bButton.GetComponent<Button>().interactable = false;
        cButton.GetComponent<Button>().interactable = false;
        dButton.GetComponent<Button>().interactable = false;
    }
}
