using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool pause = false;
    private void Start()
    {
        pause = false;
        PlayerPrefs.SetInt("Pause", 0);
    }
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            PauseToggle();
        }
    }
    public void PauseToggle()
    {
        if (pause == false)
        {
            pause = true;
            PlayerPrefs.SetInt("Pause",1);
        }
        else
        {
            pause = false;
            PlayerPrefs.SetInt("Pause", 0);
        }
    }
}
