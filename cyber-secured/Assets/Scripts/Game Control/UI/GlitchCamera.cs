// GlitchCamera.cs

/// <summary>
/// This script controls the glitch camera effect, as well as the start button function.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlitchCamera : MonoBehaviour
{
    public InputField name; // Drag and drop the input field for the name 
    public GameObject name_gameObject;
    public Camera cam;

    public bool glitch = false;     // flag for glitch effect

    // glitch script parameters:
    private float ag_slj = 0.0f;    // analog glitch scan line jitter
    //private float ag_vj  = 0.0f;    // -- vertical jump        ------->values were never used!
    //private float ag_hs  = 0.0f;    // -- horizontal shake     ------->values were never used!
    private float ag_cd  = 0.0f;    // -- color drift
    private float dg_i   = 0.0f;    // digital glitch intensity

    // initialization
    void Start()
    {
        
        if(cam == null)
        {
            cam = gameObject.GetComponent<Camera>();
        }
    }

    // non-input updating
    void FixedUpdate()
    {
        // glitch transition
        if(glitch)
        {   
            ag_slj  = Mathf.MoveTowards(ag_slj, 0.3f, 0.05f);
            ag_cd   = Mathf.MoveTowards(ag_cd,  0.3f, 0.05f);
            dg_i    = Mathf.MoveTowards(dg_i,   1.0f, 0.05f);
        } else {
            ag_slj  = Mathf.MoveTowards(ag_slj, 0.0f, 0.05f);
            ag_cd   = Mathf.MoveTowards(ag_cd,  0.0f, 0.05f);
            dg_i    = Mathf.MoveTowards(dg_i,   0.0f, 0.05f);
        }

        // process glitch effect
        if(cam != null)
        {
            cam.GetComponent<Kino.AnalogGlitch>().scanLineJitter    = ag_slj;
            cam.GetComponent<Kino.AnalogGlitch>().colorDrift        = ag_cd;
            cam.GetComponent<Kino.DigitalGlitch>().intensity        = dg_i;
        }

        // when glitch transition reaches mid-point
        if(dg_i >= 1.0f)
        {
            glitch = false;

            GameObject.FindObjectOfType<SceneControllerTitle>().HideTitle();

            GameControllerV2.Instance.DisplayNP();
            GameControllerV2.Instance.DisplayMonth();
            GameControllerV2.Instance.DisplayErrorRate();

            GameControllerV2.Instance.btn_menu.gameObject.SetActive(true);

            if(GameControllerV2.Instance.GetState() == 0) // only if in title state
            {
                Debug.Log("Game start!");
                foreach(GameObject g in GameControllerV2.Instance.companies)
                {
                    g.SetActive(true);
                }

                // move options to the top of the screen
                //GameControllerV2.Instance.scn_instruct.gameObject.SetActive(false);
                GameObject.FindObjectOfType<SceneControllerTitle>().DisableInstruct();
                /*GameControllerV2.Instance.scn_options.transform.SetPositionAndRotation(
                    new Vector3(0, 10, 0), Quaternion.identity);

                // disable the return option
                GameControllerV2.Instance.btn_return_options.gameObject.SetActive(false);

                // enable the game reset button
                GameControllerV2.Instance.btn_reset_game.gameObject.SetActive(true);
                GameControllerV2.Instance.btn_return_main.gameObject.SetActive(true);*/

                // change state to main
                GameControllerV2.Instance.SetState(3);
            }
        }
    }

    public void StartGlitch()
    {
        if(name.textComponent.text != "")
        {
            name_gameObject.SetActive(false);

            glitch = true;

            if (GameControllerV2.Instance.GetState() == 0) // only if in title state
            {
                GameObject.Find("scn_title_CONTROL").GetComponent<SceneControllerTitle>().SwitchTitleButtons();
                GameObject.Find("dlg_start").GetComponent<DialogueTrigger>().TriggerDialogue();
                FindObjectOfType<MainUIController>().AdjustMenuOnStart();
            }

            // play a glitch sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(0);
        }
    }
}
