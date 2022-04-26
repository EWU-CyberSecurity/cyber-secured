using UnityEngine;
using UnityEngine.UI;

public class FinalQuestionManager : MonoBehaviour {
    public Button correctButton;
    public Button incorrectButton;

    public GameObject next;

    // Update is called once per frame
    void Update() {
        if (correctButton.GetComponent<VirusChoiceManager>().disable ||
            incorrectButton.GetComponent<VirusChoiceManager>().disable) {
            disableButtons();
            next.SetActive(true);
        }
    }

    void disableButtons() {
        correctButton.GetComponent<Button>().interactable = false;
        incorrectButton.GetComponent<Button>().interactable = false;
    }
}
