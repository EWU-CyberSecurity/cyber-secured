using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if( aButton.GetComponent<ButtonManager> ().disable == true ||
		    bButton.GetComponent<ButtonManager> ().disable == true ||
		    cButton.GetComponent<ButtonManager> ().disable == true ||
		    dButton.GetComponent<ButtonManager> ().disable == true) 
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
