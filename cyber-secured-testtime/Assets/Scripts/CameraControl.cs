using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera testCamera;
    public Camera officeCamera;
    public GameObject nameHolder;
    public int delay = -1;

    // Update is called once per frame
    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            PlayerPrefs.DeleteAll();
            //ToggleCamera();
        }
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
