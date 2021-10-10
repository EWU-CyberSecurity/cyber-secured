using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhishingQuestionManager : MonoBehaviour {
    public Button correctButton;
    public Button incorrectButton;

    public GameObject next;

    // Update is called once per frame
    void Update() {
        if (correctButton.GetComponent<ChoiceManager>().disable == true ||
            incorrectButton.GetComponent<ChoiceManager>().disable == true) {
            disableButtons();
            next.SetActive(true);
        }
    }

    void disableButtons() {
        correctButton.GetComponent<Button>().interactable = false;
        incorrectButton.GetComponent<Button>().interactable = false;
    }
}
