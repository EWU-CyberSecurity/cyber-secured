using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public Button this_button;

	// Use this for initialization
	void Start()
    {
        Button btn = this_button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}

    void TaskOnClick()
    {
        Debug.Log("You have clicked this button!");
    }
}
