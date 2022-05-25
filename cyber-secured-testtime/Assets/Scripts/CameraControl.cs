using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //this just deals with the two cameras of the scene

    public Camera testCamera;
    public Camera officeCamera;
    public GameObject nameHolder;//name holder makes it so the game doesn't start if no name is plugged in
    public int delay = -1;

    void Update()
    {
        if (PlayerPrefs.GetInt("ManualReset") == 1 && testCamera.enabled)
        {
            ToggleCamera();
        }
        if(delay >= 0)
        {
            delay--;
            float delayPercent = (100.0f -  (float)delay)/100.0f;
            testCamera.rect = new Rect(0.5f +(1- delayPercent)*0.25f , 0.2f +(1- delayPercent) * 0.1f, delayPercent * 0.5f, delayPercent * 0.5f);
        }
    }
    public void ToggleCamera()
    {
        if (testCamera.enabled)
        {
            testCamera.enabled = false;
            testCamera.rect = new Rect(0.5f, 5f, 0.5f, 1);
        }
        else
        {
            testCamera.enabled = true;
            delay = 100;
        }
    }
    public void ToggleCameraStart()
    {
        //if no name entered dont start
        if(!(nameHolder.GetComponent<UnityEngine.UI.Text>().text == "") && !(nameHolder.GetComponent<UnityEngine.UI.Text>().text == ""))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("name", nameHolder.GetComponent<UnityEngine.UI.Text>().text);
            if (testCamera.enabled)
            {
                testCamera.enabled = false;
                testCamera.rect = new Rect(0.5f, 5f, 0.5f, 1);
            }
            else
            {
                testCamera.enabled = true;
                testCamera.rect = new Rect(0.5f, 0.0f, 0.5f, 0.885f);
            }

        }
    }
}
