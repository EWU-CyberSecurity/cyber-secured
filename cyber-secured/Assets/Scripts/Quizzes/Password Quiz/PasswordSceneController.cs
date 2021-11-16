using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Topics;
using UnityEngine;

public class PasswordSceneController : MonoBehaviour
{
    private DialogueManager dialogue;
    private GameObject scn_main;

    public int round; // The current round of the quiz. -1 is a convenient initial value here
                      // that represents the fact that the quiz has not started. This is set in
                      // the scn_quiz_password game object.

    public int lives;   // when lives = 0, you lose the minigame

    // Going to do this without using the Topic class at first. 
    // Also not going to use TopicItem, just the questions. 
    private List<Question> questions = new List<Question>();
    private Question currentQuestion;

    private readonly string[] explanation = {
        "\"I am infatuated with you\" is much longer and uses spaces. \"I am infatuated with you\" can be improved so it doesn't have words; Eg: \"i^m infat 2 ated w/ y0u\"",
        "Since \"2 b OR !tuby\" uses numbers, capital and lowercase letters, and symbols, it is more complex than the other passwords.",
        "\"RoundRobin\" is the worst of these options because a hacker could assume the bank's name was used in the password somehow because it is easy to remember.",
        "\"H20 b@ttle\" cannot be found in the dictionary, and seems more random than the others. dihydrogenmonoxide can be strong because of length, but it's technically a 2-word combo.",
        "A Password Manager is secure software that handles your passwords for you. Helping your co-worker remember his password is better than leaving a password in readable form."
    };

    private readonly string[] affirmation = { "Bingo! ", "That's correct! ", "Good job. ", "You got it. ", "Nice work. " };
    private readonly string[] disdain = { "That's not it :( ", "That's incorrect! ", "Actually... ", "There's a better answer. ", "Try again! " };

    // Use this for initialization
    void Awake()
    {
        setUpQuestions();

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

    private void setUpQuestions()
    {
        Question question1 = new Question("Which password is hardest to crack?");
        MultiAnswer answer1 = new MultiAnswer();
        MultiAnswerSet answerSet1 = new MultiAnswerSet("I am infatuated with you",
            "eyeluvewe",
            "ILoooveYou",
            "I<3You", new[] { 0 });

        answer1.addToAnswerPool(answerSet1);
        question1.setAnswer(answer1);

        questions.Add(question1);

        Question question2 = new Question("Which of these 12 character long passwords is the hardest to brute force?");
        MultiAnswer answer2 = new MultiAnswer();
        MultiAnswerSet answerSet2 = new MultiAnswerSet("2 b OR !tuby",
            "1-8002446227",
            "abcdefghijkl",
            "qwertyuiop[]", new[] { 0 });
        
        answer2.addToAnswerPool(answerSet2);
        question2.setAnswer(answer2);
        questions.Add(question2);

        Question question3 = new Question("You need a new strong password for your Robin bank account. Which password would be the WORST to use?");
        MultiAnswer answer3 = new MultiAnswer();
        MultiAnswerSet answerSet3 = new MultiAnswerSet("RoundRobin",
            "RoundCactus",
            "RoundLemur",
            "RoundCircle", new[] { 0 });

        answer3.addToAnswerPool(answerSet3);
        question3.setAnswer(answer3);
        questions.Add(question3);

        Question question4 = new Question("Suppose your password is \"waterbottle\"," +
                                          " and you want to make it more secure. Which password would be the best " +
                                          " improvement to security?");
        MultiAnswer answer4 = new MultiAnswer();
        MultiAnswerSet answerSet4 = new MultiAnswerSet("H20 b@ttle",
            "waterbottle!",
            "water bottle",
            "dihydrogenmonoxide", new[] { 0, 3 });

        answer4.addToAnswerPool(answerSet4);
        question4.setAnswer(answer4);
        questions.Add(question4);

        Question question5 = new Question("You know your co-worker writes his password down because it is very" +
                                          " complicated and hard to remember. What could you suggest to him to do instead?");
        MultiAnswer answer5 = new MultiAnswer();
        MultiAnswerSet answerSet5 = new MultiAnswerSet("Password Manager",
            "Make a new password",
            "Create mnenomic",
            "Store in .txt file", new[] { 0, 1, 2 });

        answer5.addToAnswerPool(answerSet5);
        question5.setAnswer(answer5);
        questions.Add(question5);
    }

    void Start()
    {
        lives = 3;
    }

    public void nextQuestion()
    {
        round++;
        // if the quiz is over then the continue button ends it otherwise go to the next question
        if (round == questions.Count)
        {
            // the quiz is complete
            GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
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

            AudioControllerV2 audioController = GameObject.Find("SoundManager").GetComponent<AudioControllerV2>();
            audioController.PlayGameMusic();
        }
        else
        {
            // this quiz is all questions, so that's why this variable isn't called currentItem or something.
            currentQuestion = questions.ElementAt(round);
            currentQuestion.startItem();
        }
    }

    public void onMultiAnswerButton1Clicked()
    {
        // Once again we know that this is a multi answer question.
        // These methods could be refactored to just have one function with
        // an argument and a switch statement.
        bool correct = ((MultiAnswer) currentQuestion.getAnswer()).onButton1Clicked();
        displayAnswerFeedback(correct);
    }

    public void onMultiAnswerButton2Clicked()
    {
        bool correct = ((MultiAnswer)currentQuestion.getAnswer()).onButton2Clicked();
        displayAnswerFeedback(correct);
    }

    public void onMultiAnswerButton3Clicked()
    {
        bool correct = ((MultiAnswer)currentQuestion.getAnswer()).onButton3Clicked();
        displayAnswerFeedback(correct);
    }

    public void onMultiAnswerButton4Clicked()
    {
        bool correct = ((MultiAnswer)currentQuestion.getAnswer()).onButton4Clicked();
        displayAnswerFeedback(correct);
    }

    public void displayAnswerFeedback(bool correct)
    {
        int random = Random.Range(0, 5);

        if (correct)
        {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

            currentQuestion.changeQuestionText(affirmation[random] + explanation[round]);

            // increase NP by 2
            GameControllerV2.Instance.IncreaseNP(2);
        }
        else
        {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);

            GameObject.Find("scn_quiz_password").GetComponent<PasswordSceneController>().DecreaseLife();
            currentQuestion.changeQuestionText(disdain[random] + explanation[round]);
        }

        currentQuestion.showContinueButton();
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
