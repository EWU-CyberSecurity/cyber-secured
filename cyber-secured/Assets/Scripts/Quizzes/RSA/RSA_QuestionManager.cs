using UnityEngine;
using UnityEngine.UI;

public class RSA_QuestionManager : MonoBehaviour
{
    public Button aButton;
    public Button bButton;
    public Button cButton;
    public Button dButton;

    public GameObject next;
    
    // Update is called once per frame
    void Update()
    {
        if( aButton.GetComponent<RSA_ButtonManager> ().disable ||
            bButton.GetComponent<RSA_ButtonManager> ().disable ||
            cButton.GetComponent<RSA_ButtonManager> ().disable ||
            dButton.GetComponent<RSA_ButtonManager> ().disable) 
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
