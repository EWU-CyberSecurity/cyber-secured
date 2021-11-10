using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Topics;
using UnityEngine;

public class PasswordSceneController : MonoBehaviour
{
    private DialogueManager dialogue;
    private GameObject scn_main;

    public int lives;   // when lives = 0, you lose the minigame

    // Going to do this without using the Topic class at first. 
    // Also not going to use TopicItem, just the questions. 
    private List<Question> questions = new List<Question>();
    private Question currentQuestion;
    
    // Use this for initialization
    void Awake()
    {
        Question question1 = new Question("Which password is hardest to crack?");
        MultiAnswer answer1 = new MultiAnswer();
        MultiAnswerSet answerSet1 = new MultiAnswerSet("I am infatuated with you",
                                                "eyeluvewe",
                                                "ILoooveYou",
                                                "I<3You", new[] {0});
        answer1.addToAnswerPool(answerSet1);
        question1.setAnswer(answer1);

        questions.Add(question1);

        // displays opening text
        GameObject.Find("dlg_password_intro").GetComponent<DialogueTrigger>().TriggerDialogue();

        // glitch animation
        FindObjectOfType<GlitchCamera>().StartGlitch();

        //Deactivating the scn_main to show the animation better without the background:
        scn_main = GameObject.Find("scn_main");

        scn_main.SetActive(false);

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    public void nextQuestion()
    {
        if (currentQuestion == null)
        {
            currentQuestion = questions.ElementAt(0);
        }
        else
        {
            currentQuestion = questions.ElementAt(questions.IndexOf(currentQuestion) + 1);
        }

        currentQuestion.startItem();
    }

    void Start()
    {
        lives = 3;
    }

    public void DecreaseLife()
    {
        lives--;
        // if the player has failed the minigame
        if (lives <= 0)
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

            AudioControllerV2 audioController = GameObject.Find("SoundManager").GetComponent<AudioControllerV2>();
            audioController.PlayGameMusic();

            // don't need script after this
            Destroy(this);
        }
    }
}
