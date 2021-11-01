using UnityEngine;
using UnityEngine.UI;

public class PhishingQuestionManager : MonoBehaviour {
    public Button correctButton;
    public Button incorrectButton;

    public GameObject next;

    // Update is called once per frame
    void Update() {
        if (correctButton.GetComponent<ChoiceManager>().disable ||
            incorrectButton.GetComponent<ChoiceManager>().disable) {
            disableButtons();
            next.SetActive(true);
        }
    }

    void disableButtons() {
        correctButton.GetComponent<Button>().interactable = false;
        incorrectButton.GetComponent<Button>().interactable = false;
    }
}
