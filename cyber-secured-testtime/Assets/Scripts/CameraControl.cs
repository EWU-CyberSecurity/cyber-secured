﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera testCamera;
    public Camera officeCamera;
    public GameObject nameHolder;


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
            testCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 1);
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
                testCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 1);
            }

        }
    }
}
