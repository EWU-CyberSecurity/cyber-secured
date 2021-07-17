using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VirusQuizManager : MonoBehaviour {
    public GameObject [] questions = new GameObject[5];
    public GameObject next;

    public int i = 0;

    // Use this for initialization
    void Start() {

        // hides continue button
        next.SetActive(false);

        questions[0].SetActive(true);

        // set rounds 2 to 5 inactive
        for (int j = 1; j < 5; j++) {
            questions[j].SetActive(false);
        }

    }

    // proceeds to next question
    public void nextSet() {
        next.SetActive(false);

        if (i < 5) {
            questions[i].SetActive(false);
        }
        if (i != 4) {
            questions[i + 1].SetActive(true);
            i++;
        } else {
            // glitch screen
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

            // deactivate phishing minigame
            GameControllerV2.Instance.scn_virus_quiz.SetActive(false);

            // reward for completion
            GameControllerV2.Instance.current_decision_text = "Your employees learn to fight threats. " +
                "<i>Error rate has decreased.</i>";
            // error rate decreased by 5-10%
            // TODO: may need adjustments
            float rand_er = Random.Range(0.05f, 0.1f);
            GameControllerV2.Instance.DecreaseErrorRate(rand_er);

            // deactivate quiz, and display results
            GameControllerV2.Instance.scn_virus_quiz.SetActive(false);
            GameControllerV2.Instance.DisplayDecision();
        }
    }
}
