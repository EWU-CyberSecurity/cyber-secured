using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera testCamera;
    public Camera officeCamera;


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
            ToggleCameraAndSmaller();
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
    public void ToggleCameraAndSmaller()
    {
        if (testCamera.enabled)
            testCamera.enabled = false;
        else
            testCamera.enabled = true;
        testCamera.rect = new Rect(0.5f, 0.5f, 0.5f, 1);
    }
}
