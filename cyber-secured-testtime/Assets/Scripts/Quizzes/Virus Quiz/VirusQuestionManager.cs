using UnityEngine;
using UnityEngine.UI;

public class VirusQuestionManager : MonoBehaviour {
    public Button aButton;
    public Button bButton;
    public Button cButton;
    public Button dButton;

    public GameObject next;

    // Update is called once per frame
    void Update() {
        
        if (aButton.GetComponent<VirusChoiceManager>().disable ||
            bButton.GetComponent<VirusChoiceManager>().disable ||
            cButton.GetComponent<VirusChoiceManager>().disable ||
            dButton.GetComponent<VirusChoiceManager>().disable) 
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
