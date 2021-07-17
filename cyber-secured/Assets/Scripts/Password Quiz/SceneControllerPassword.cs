using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerPassword : MonoBehaviour
{
    public int lives;   // when lives = 0, you lose the minigame

    void Start()
    {
        lives = 3;
    }

    public void DecreaseLife()
    {
        lives--;

        // if the player has failed the minigame
        if(lives <= 0)
        {
            GameObject.Find("dlg_quiz_done").GetComponent<DialogueTrigger>().TriggerDialogue();

            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // punishment
            GameControllerV2.Instance.current_decision_text = "Your employees failed to " +
                "learn how to create a good password. " +
                "<i>Error rate has increased.</i>";

            // error rate increased by 5-10%
            // TODO: may need adjustments
            float rand_er = Random.Range(0.05f, 0.1f);
            GameControllerV2.Instance.IncreaseErrorRate(rand_er);

            // deactivate quiz, and display results
            GameControllerV2.Instance.scn_quiz_password.SetActive(false);
            GameControllerV2.Instance.DisplayDecision();

            GameControllerV2.Instance.scn_main.SetActive(true);

            // don't need script after this
            Destroy(this);
        }
    }
}
