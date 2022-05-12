using UnityEngine;

public class RSA_QuizManager : MonoBehaviour
{
    public GameObject [] questions;
    public GameObject next;
    public GameObject menuButton; // drag and drop the menu button to here
    public GameObject hintButton; // drag and drop the hint button (at the top of the questions) to here

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
                
                menuButton.SetActive(true); // show again the button
                hintButton.SetActive(false);

                // done with quiz, succeeded in not failing
                // glitch screen
                GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

                // reward for completion
                GameControllerV2.Instance.current_decision_text = "Your employees learned the importance of RSA encryption. " +
                    "\nCongrats!" +
                    "\n<i>Error rate has decreased.</i>";
                // error rate decreased by 5-10%
                // TODO: may need adjustments
                float rand_er = Random.Range(0.05f, 0.1f);
                GameControllerV2.Instance.DecreaseErrorRate(rand_er);

                // deactivate quiz, and display results
                GameControllerV2.Instance.scn_quiz_password.SetActive(false);
                GameControllerV2.Instance.DisplayDecision();

                audioController.PlayGameMusic();
            }
        }
    }
}
