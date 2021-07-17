using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalQuestionManager : MonoBehaviour {
    public Button correctButton;
    public Button incorrectButton;

    public GameObject next;

    // Update is called once per frame
    void Update() {
        if (correctButton.GetComponent<VirusChoiceManager>().disable == true ||
            incorrectButton.GetComponent<VirusChoiceManager>().disable == true) {
            disableButtons();
            next.SetActive(true);
        }
    }

    void disableButtons() {
        correctButton.GetComponent<Button>().interactable = false;
        incorrectButton.GetComponent<Button>().interactable = false;
    }
}
