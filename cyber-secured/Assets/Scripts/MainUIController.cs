// MainUIController.cs

/// <summary>
/// This script controls a portion of the UI of the main scene.
/// GameControllerV2.cs controls the Decision and Event UI.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// imported scripts:
using DG.Tweening;

public class MainUIController : MonoBehaviour
{
    public Text display_np;                     // --
    public Text display_error_rate;             // --

    public Button btn_menu;                     // --

    public GameObject scn_options;              // options menu
    public Button btn_return_options;           // --

    public Button btn_reset_game;               // --
    public Button btn_return_main;              // --
    public Button btn_skip;                     // -- 

    public Text txt_color_ref;                  // text for color reference

    private Color color_original;               // original color of text

	// Use this for initialization
	void Start()
	{
        color_original = txt_color_ref.color;
	}

    // non-input updating
    void FixedUpdate()
    {
        if(display_np.fontSize > 30)
        {
            display_np.fontSize--;
        }

        if(display_error_rate.fontSize > 30)
        {
            display_error_rate.fontSize--;
        }
    }

    // adjusts menu when game starts
    public void AdjustMenuOnStart()
    {
        // moves options to the top of the screen
        scn_options.transform.SetPositionAndRotation(
            new Vector3(0, 10, 0), Quaternion.identity);

        // disable the return option
        btn_return_options.gameObject.SetActive(false);

        // enable the game reset button
        btn_reset_game.gameObject.SetActive(true);
        btn_return_main.gameObject.SetActive(true);
        btn_skip.gameObject.SetActive(true);
    }

    // sequence to make NP pop
    public void PopText(Text t, bool x)
    {
        t.fontSize = 40;
        Sequence my_sequence = DOTween.Sequence();
        Color32 new_color;

        if(x)
        {
            new_color = new Color32(147, 255, 150, 255); // green-ish
        } else {
            new_color = new Color32(232, 160, 134, 255); // red-ish
        }

        my_sequence.Append(t.DOColor(new_color, 0.5f));
        my_sequence.Append(t.DOColor(color_original, 0.5f));
    }

    public void MenuButton()
    {
        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        // moves all the things appropriately down
        // TODO: when finished with all scenes, create an array with all scenes and for loop this
        GameControllerV2.Instance.scn_main.transform.DOLocalMoveY         (-700, 0.8f);
        GameControllerV2.Instance.scn_quiz_password.transform.DOLocalMoveY(-500, 0.8f);
        GameControllerV2.Instance.scn_filebackup.transform.DOLocalMoveY   (-500, 0.8f);
        GameControllerV2.Instance.scn_phishing_v2.transform.DOLocalMoveY(-500, 0.8f);
        GameControllerV2.Instance.scn_virus_presentation.transform.DOLocalMoveY(-500, 0.8f);
        GameControllerV2.Instance.scn_virus_quiz.transform.DOLocalMoveY(-500, 0.8f);
        GameControllerV2.Instance.scn_caesar_cipher.transform.DOLocalMoveY(-500, 0.8f);

        scn_options.transform.DOLocalMoveY      (0, 0.75f);
        btn_menu.transform.DOLocalMoveY         (100, 0.8f);
    }

    public void ReturnButton()
    {
        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        // moves all the things appropriately up
        GameControllerV2.Instance.scn_main.transform.DOLocalMoveY         (0, 0.8f);
        GameControllerV2.Instance.scn_quiz_password.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_filebackup.transform.DOLocalMoveY   (0, 0.8f);
        GameControllerV2.Instance.scn_phishing_v2.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_virus_presentation.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_virus_quiz.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_caesar_cipher.transform.DOLocalMoveY(0, 0.8f);

        scn_options.transform.DOLocalMoveY      (500, 0.75f);
        btn_menu.transform.DOLocalMoveY         (275, 0.8f);
    }

    public void MarketButton()
    {
        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        scn_options.transform.DOLocalMoveY(-700, 0.75f);
        btn_menu.transform.DOLocalMoveY(100, 0.8f);
    }

    public void SkipButton()
    {
        // Beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        // moves all the things appropriately up
        GameControllerV2.Instance.scn_main.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_quiz_password.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_filebackup.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_phishing_v2.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_virus_presentation.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_virus_quiz.transform.DOLocalMoveY(0, 0.8f);
        GameControllerV2.Instance.scn_caesar_cipher.transform.DOLocalMoveY(0, 0.8f);

        scn_options.transform.DOLocalMoveY(500, 0.75f);
        btn_menu.transform.DOLocalMoveY(275, 0.8f);

        // Skips the current quiz scene
        if (GameControllerV2.Instance.scn_quiz_password.activeSelf)
        {
            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // deactivate quiz, display decision, increase error rate 10%
            GameControllerV2.Instance.scn_quiz_password.SetActive(false);
            GameControllerV2.Instance.current_decision_text = "You give up on learning to create a good password.. " +
                    "<i>Error rate has increased.</i>";
            GameControllerV2.Instance.IncreaseErrorRate(.1f);
            GameControllerV2.Instance.DisplayDecision();
        }

        // Skips the current quiz scene
        if (GameControllerV2.Instance.scn_phishing_v2.activeSelf)
        {
            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // deactivate quiz, display decision, increase error rate 10%
            GameControllerV2.Instance.scn_phishing_v2.SetActive(false);
            GameControllerV2.Instance.current_decision_text = "You give up on learning to distinguish emails " +
                    "<i>Error rate has increased.</i>";
            GameControllerV2.Instance.IncreaseErrorRate(.1f);
            GameControllerV2.Instance.DisplayDecision();
        }

        // Skips the current quiz scene
        if (GameControllerV2.Instance.scn_virus_quiz.activeSelf)
        {
            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // deactivate quiz, display decision, increase error rate 10%
            GameControllerV2.Instance.scn_virus_quiz.SetActive(false);
            GameControllerV2.Instance.current_decision_text = "You give up on your virus quiz.. " +
                    "<i>Error rate has increased.</i>";
            GameControllerV2.Instance.IncreaseErrorRate(.1f);
            GameControllerV2.Instance.DisplayDecision();
        }

        // Skips the current quiz scene
        if (GameControllerV2.Instance.scn_caesar_cipher.activeSelf)
        {
            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // deactivate quiz, display decision, increase error rate 10%
            GameControllerV2.Instance.scn_caesar_cipher.SetActive(false);
            GameControllerV2.Instance.current_decision_text = "You give up on learning about encyrption " +
                    "<i>Error rate has increased.</i>";
            GameControllerV2.Instance.IncreaseErrorRate(.1f);
            GameControllerV2.Instance.DisplayDecision();
        }
    }
}
