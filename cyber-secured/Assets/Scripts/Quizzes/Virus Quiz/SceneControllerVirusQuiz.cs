using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerVirusQuiz : MonoBehaviour {

    public int lives;

    // Use this for initialization
    void Start() {
        lives = 3;

        // displays opening text
        GameObject.Find("dlg_virus_quiz").GetComponent<DialogueTrigger>().TriggerDialogue();

        // glitch animation
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
    }

    public void DecreaseLife() {
        lives--;

        if (lives == 0) {

            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // punishment
            GameControllerV2.Instance.current_decision_text = "Your employees failed the quiz!" + "\n<i>Error rate has increased.</i>";
            // error rate increased by 5-10%
            // TODO: may need adjustments
            float rand_er = Random.Range(0.05f, 0.1f);
            GameControllerV2.Instance.IncreaseErrorRate(rand_er);

            // deactivate quiz, and display results
            GameControllerV2.Instance.scn_virus_quiz.SetActive(false);
            GameControllerV2.Instance.DisplayDecision();

            // don't need script after this
            Destroy(this);
        }
    }
}