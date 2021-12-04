// GlitchCamera.cs

/// <summary>
/// This script essentially transitions between stages in the game.
/// It used to have a glitch effect that was used as a transition, but we removed it.
/// This class wasn't renamed though. And the actual purpose of both of the methods isn't 100% understood by us.
/// TODO: Rename this script, and clean it up so it's more clear what's happening.
/// TODO: Maybe move the bottom function into GameController because it does nothing most of the time.
/// TODO: And call the other function something like TransitionFromQuiz().
/// - Team 1
/// </summary>

using UnityEngine;
using UnityEngine.UI;

public class GlitchCamera : MonoBehaviour
{
    public InputField name_field; // Drag and drop the input field for the name 
    public GameObject name_gameObject;
    public Camera cam;

    public bool update = false;     // flag for update

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
        // when glitch transition reaches mid-point
        if(update)
        {
            update = false;

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
        if(!string.IsNullOrEmpty(name_field.textComponent.text))
        {
            name_gameObject.SetActive(false);

            update = true;

            if (GameControllerV2.Instance.GetState() == 0) // only if in title state
            {
                GameObject.Find("scn_title_CONTROL").GetComponent<SceneControllerTitle>().SwitchTitleButtons();
                GameObject.Find("dlg_start").GetComponent<DialogueTrigger>().TriggerDialogue();
                FindObjectOfType<MainUIController>().AdjustMenuOnStart();

                GameObject.Find("scn_title").transform.Find("btn_till_end").gameObject.SetActive(true);
            }
        }
    }
}
