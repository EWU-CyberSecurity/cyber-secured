// SceneControllerTitle.cs

/// <summary>
/// This script controls the title screen; in tandem with GlitchCamera.cs
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// imported scripts
using DG.Tweening;

public class SceneControllerTitle : MonoBehaviour
{
    
    public GameObject background;               // background image

    public GameObject scn_title;                // title scene
    public Text txt_title;                      // --
    public Button btn_start;                    // --
    public Button btn_instruct;                 // --
    public Button btn_options;                  // --
    public Button btn_about;                    // About button.

    public GameObject scn_instruct;             // instructions scene
    public Button btn_return_instruct;          // --

    public GameObject scn_options;              // options menu
    public Button btn_return_options;           // --
    public Button btn_reset_game;               // --
    public Button btn_return_main;              // --
    public Button btn_return;                   // return button in options menu that only appear after game is started

    public GameObject scn_about;                // instructions scene
    public Button btn_return_about;             // --

    // Use this for initialization
    void Start()
    {
        btn_return.gameObject.SetActive(false);
    }

    // button function to see instructions
    public void DisplayInstructions()
    {
        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        // move to instructions area to the middle of the screen, and move title screen over
        scn_instruct.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
        scn_title.transform.DOLocalMove(new Vector3(1200, 0, 0), 0.7f);
        background.transform.DOLocalMove(new Vector3(0, 0 ,0), 0.7f);

        // disable title buttons
        SwitchTitleButtons();

        // change state to instructions
        GameControllerV2.Instance.SetState(1);
    }

    // button function to see options
    // TODO: make options functional
    public void DisplayOptions()
    {
        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        // move to instructions area to the middle of the screen, and move title screen over
        scn_options.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
        scn_title.transform.DOLocalMove(new Vector3(-1200, 0, 0), 0.7f);
        background.transform.DOLocalMove(new Vector3(0, 0 ,0), 0.7f);

        // disable title buttons
        SwitchTitleButtons();

        // change states to options
        //CURRENT_STATE = State.options;
        GameControllerV2.Instance.SetState(2);
    }

    public void DisplayAbouts()
    {
        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        // move to instructions area to the middle of the screen, and move title screen over
        scn_about.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
        scn_title.transform.DOLocalMove(new Vector3(0, 1200, 0), 0.7f);
        background.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);

        // disable title buttons
        SwitchTitleButtons();

        // change state to instructions
        GameControllerV2.Instance.SetState(5);
    }

    // button function to move back to title screen
    public void MoveToTitle()
    {
        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        // either in instruction scene or options scene
        if(GameControllerV2.Instance.GetState() == 1)
        {
            // move title and instructions back to place
            scn_instruct.transform.DOLocalMove(new Vector3(-1200, 0, 0), 0.7f);
            scn_title.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
            background.transform.DOLocalMove(new Vector3(0, 0 ,0), 0.7f);
        } else if(GameControllerV2.Instance.GetState() == 2) {
            // move title and options menu back to place
            scn_options.transform.DOLocalMove(new Vector3(1200, 0, 0), 0.7f);
            scn_title.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
            background.transform.DOLocalMove(new Vector3(0, 0 ,0), 0.7f);
        } else if (GameControllerV2.Instance.GetState() == 5) {
            // move title and about menu back to place
            scn_about.transform.DOLocalMove(new Vector3(0, -1200, 0), 0.7f);
            scn_title.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
            background.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
        } else {
            scn_options.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
            scn_title.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
            background.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f);
        }
          

        // enable title buttons
        SwitchTitleButtons();

        // change state back to title
        //CURRENT_STATE = State.title;
        GameControllerV2.Instance.SetState(0);
    }

    // enables or disables title buttons
    public void SwitchTitleButtons()
    {
        // use start button to check all three buttons
        if(btn_start.interactable)
        {
            // disable title screen buttons
            btn_start.interactable = false;
            btn_instruct.interactable = false;
            btn_options.interactable = false;
            btn_about.interactable = false;

            // enable the return buttons
            btn_return_instruct.interactable = true;
            btn_return_options.interactable = true;
            btn_return_about.interactable = true;
        } else {
            // enable title screen buttons
            btn_start.interactable = true;
            btn_instruct.interactable = true;
            btn_options.interactable = true;
            btn_about.interactable = true;

            // disable the return buttons
            btn_return_instruct.interactable = false;
            btn_return_options.interactable = false;
            btn_return_about.interactable = false;
        }
    }
    
    // disables instructions scene
    public void DisableInstruct()
    {
        scn_instruct.gameObject.SetActive(false);
    }

    public void HideTitle()
    {
        // deactivate title objects; to main game state
        txt_title.gameObject.SetActive(false);
        btn_start.gameObject.SetActive(false);
        btn_instruct.gameObject.SetActive(false);
        btn_options.gameObject.SetActive(false);
        btn_about.gameObject.SetActive(false);
    }

    public void OnNameInputChanged(InputField name_input)
    {
        Button start_button = GameObject.Find("btn_start").GetComponent<Button>();

        ColorBlock new_colors = start_button.colors;
        if (string.IsNullOrEmpty(name_input.text)) {
            new_colors.highlightedColor = new Color(1, 0.7245814f, 0.7176471f);
            new_colors.pressedColor = new Color(1, 0.607629f, 0.5764706f);
            start_button.colors = new_colors;
        } else {
            // these are the original green colors
            new_colors.highlightedColor = new Color(0.8156863f, 1, 0.7176471f);
            new_colors.pressedColor = new Color(0.5764706f, 1, 0.5882353f);
            start_button.colors = new_colors;
        }
    }
}
