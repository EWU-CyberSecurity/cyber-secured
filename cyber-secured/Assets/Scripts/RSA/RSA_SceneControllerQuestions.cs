using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSA_SceneControllerQuestions : MonoBehaviour
{
    public int lives;   // when lives = 0, you lose the minigame
    public GameObject back_btn;

    void Start()
    {
        lives = 3;
        back_btn.SetActive(true);
    }

    public void DecreaseLife()
    {
        Debug.Log(lives);

        lives--;

        // if the player has failed the minigame
        if(lives <= 0)
        {
            GameObject.Find("dlg_RSA_questions").GetComponent<DialogueTrigger>().TriggerDialogue();

            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // punishment
            GameControllerV2.Instance.current_decision_text = "Your employees failed to " +
                "learn the importance of RSA encryption. " +
                "<i>Error rate has increased.</i>";

            // error rate increased by 5-10%
            // TODO: may need adjustments
            float rand_er = Random.Range(0.05f, 0.1f);
            GameControllerV2.Instance.IncreaseErrorRate(rand_er);

            // deactivate quiz, and display results
            GameControllerV2.Instance.scn_RSA.SetActive(false);
            GameControllerV2.Instance.DisplayDecision();

            // don't need script after this
            Destroy(this);
        }
    }
}
