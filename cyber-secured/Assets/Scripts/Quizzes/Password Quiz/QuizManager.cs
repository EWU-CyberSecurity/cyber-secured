using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject [] questions;
    public GameObject next;

    public int i = 0;

    private AudioControllerV2 audioController;

    // Use this for initialization
    void Start()
    {
        audioController = GameObject.Find("SoundManager").GetComponent<AudioControllerV2>();
        audioController.PlayQuizMusic();

        next.SetActive (false);

        questions = GameObject.FindGameObjectsWithTag("question");
        for(int j = 1; j < 5; j++)
        {
            questions[j].SetActive(false);
        }
    }

    public void nextSet()
    {
        next.SetActive(false);

        if(i < 5)
        {
            questions[i].SetActive(false);
            if(i != 4)
            {
                questions[i + 1].SetActive(true);
                i++;
            } else {
                // done with quiz, succeeded in not failing
                // glitch screen
                //GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
                GameObject.Find("dlg_quiz_success").GetComponent<DialogueTrigger>().TriggerDialogue();
                
                // reward for completion
                GameControllerV2.Instance.current_decision_text = "Your employees learn to create a good password. " +
                    "<i>Error rate has decreased.</i>";
                // error rate decreased by 5-10%
                // TODO: may need adjustments
                float rand_er = Random.Range(0.05f, 0.1f);
                GameControllerV2.Instance.DecreaseErrorRate(rand_er);

                // deactivate quiz, and display results
                GameControllerV2.Instance.scn_quiz_password.SetActive(false);
                GameControllerV2.Instance.DisplayDecision();
                GameControllerV2.Instance.scn_main.SetActive(true);

                audioController.PlayGameMusic();
            }
        }
    }
}
