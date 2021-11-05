using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Button aButton;
    public Button bButton;
    public Button cButton;
    public Button dButton;

    public GameObject next;
    
    // Update is called once per frame
    void Update()
    {
        if( aButton.GetComponent<ButtonManager> ().disable ||
            bButton.GetComponent<ButtonManager> ().disable ||
            cButton.GetComponent<ButtonManager> ().disable ||
            dButton.GetComponent<ButtonManager> ().disable) 
        {
            disableButtons();
            next.SetActive(true);
        }
    }
        
    void disableButtons()
    {
        aButton.GetComponent<Button> ().interactable = false;
        bButton.GetComponent<Button> ().interactable = false;
        cButton.GetComponent<Button> ().interactable = false;
        dButton.GetComponent<Button> ().interactable = false;
    }
}
