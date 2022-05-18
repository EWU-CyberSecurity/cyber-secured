﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Topics;
using UnityEngine;

public class PasswordSceneController : MonoBehaviour
{
    private DialogueManager dialogue;
    private GameObject scn_main;
    public GameObject gameControl;

    public int round; // The current round of the quiz. -1 is a convenient initial value here
                      // that represents the fact that the quiz has not started. This is set in
                      // the scn_quiz_password game object.

    public int lives;   // when lives = 0, you lose the minigame

    // Going to do this without using the Topic class at first. 
    // Also not going to use TopicItem, just the questions. 
    private List<Question> questions = new List<Question>();
    private Question currentQuestion;

    private readonly string[] affirmation = { "Bingo! ", "That's correct! ", "Good job. ", "You got it. ", "Nice work. " };
    private readonly string[] disdain = { "That's not it :( ", "That's incorrect! ", "There's a better answer. "};

    // Use this for initialization
    void Awake()
    {
        setUpQuestions();

        // displays opening text
        GameObject.Find("dlg_password_intro").GetComponent<DialogueTrigger>().TriggerDialogue();

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void setUpQuestions()
    {
        GameObject questionBox = GameObject.Find("scn_quiz_password").transform.Find("Quiz Components").transform.Find("question_box").gameObject;
        GameObject continueButton = GameObject.Find("scn_quiz_password").transform.Find("Quiz Components").transform.Find("Continue_button").gameObject;
        GameObject root = GameObject.Find("scn_quiz_password").transform.Find("Quiz Components").transform.Find("Buttons").gameObject;

        Question questionToAdd = new Question("Which password is hardest to crack?");
        questionToAdd.setContinueButton(continueButton);
        questionToAdd.setQuestionBox(questionBox);
        MultiAnswer answerToAdd = new MultiAnswer(root);
        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "I am infatuated with you", "eyeluvewe",
            "ILoooveYou", "I<3You", new[] { 0 },
            "\"I am infatuated with you\" is much longer and uses spaces. \"I am infatuated with you\" can be improved so it doesn't have words; Eg: \"i^m infat 2 ated w/ y0u\""
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "numbers one to ten", "12345678910",
            "one2ten", "0nly_nUmb3rs", new[] { 0 },
            "\"numbers one to ten\" is the longest and uses spaces, making it the strongest. It could be improved by adding numbers, symbols, or uppercase letters."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "Nobody expects the Spanish Inquisition!", "A k1ll3r r@bbit!",
            "Knights who say NI", "FleshWound1975", new[] { 0 },
            "Although all of these are strong passwords \"Nobody expects the Spanish Inquisition!\" is the strongest because of its length and use of uppercase letters and a special character."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "Unl0ck the Door!", "password",
            "this_is_my_password", "1 g00d p@ssword!", new[] { 0 },
            "\"Unl0ck the Door!\" is the strongest password due to not using the word \"password\". It also uses a number, a special character, and uses uppercase letters."
        ));

        questionToAdd.setAnswer(answerToAdd);

        questions.Add(questionToAdd);

        questionToAdd = new Question("Which of these 12 character long passwords is the hardest to brute force?");
        questionToAdd.setContinueButton(continueButton);
        questionToAdd.setQuestionBox(questionBox);
        answerToAdd = new MultiAnswer(root);
        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "2 b OR !tuby",
            "1-8002446227",
            "abcdefghijkl",
            "qwertyuiop[]", new[] { 0 },
            "Since \"2 b OR !tuby\" uses numbers, capital and lowercase letters, and symbols, it is more complex than the other passwords."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "a green sign",
            "174-01011118",
            "+ one Quest!",
            "qrstuvwxyz[]", new[] { 2 },
            "Since \"+ one Quest!\" uses special characters along with uppercase and lowercase letters it is the hardest to brute force."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "a fruit xbox",
            "187-55503645",
            "N1ne Four #s",
            "defghijklm[]", new[] { 2 },
            "Since \"N1ne Four #s\" uses special characters along with uppercase and lowercase letters it is the hardest to brute force."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "! 2 m@ny #'s",
            "248163264128",
            "goodPassword",
            "qwert1234567", new[] { 0 },
            "Since \"! 2 m@ny #'s\" uses many special characters and a number it is the hardest to brute force especially compared to the other passwords, which are very weak."
        ));

        questionToAdd.setAnswer(answerToAdd);
        questions.Add(questionToAdd);

        questionToAdd = new Question("You need a new strong password for your Robin bank account. Which password would be the WORST to use?");
        questionToAdd.setContinueButton(continueButton);
        questionToAdd.setQuestionBox(questionBox);
        answerToAdd = new MultiAnswer(root);
        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "RoundRobin",
            "RoundCactus",
            "RoundLemur",
            "RoundCircle", new[] { 0 },
            "\"RoundRobin\" is the worst of these options because a hacker could assume the bank's name was used in the password somehow because it is easy to remember."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "RoundRobin",
            "RoundTires",
            "RoundMonkey",
            "RoundShape", new[] { 0 },
            "\"RoundRobin\" is the worst of these options because a hacker could assume the bank's name was used in the password somehow because it is easy to remember."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "RoundRobin",
            "RoundStone",
            "RoundPanda",
            "RoundStars", new[] { 0 },
            "\"RoundRobin\" is the worst of these options because a hacker could assume the bank's name was used in the password somehow because it is easy to remember."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "RoundRobin",
            "RoundPlant",
            "RoundFlower",
            "RoundGlasses", new[] { 0 },
            "\"RoundRobin\" is the worst of these options because a hacker could assume the bank's name was used in the password somehow because it is easy to remember."
        ));

        questionToAdd.setAnswer(answerToAdd);
        questions.Add(questionToAdd);

        questionToAdd = new Question("Suppose your password is \"waterbottle\"," +
                                          " and you want to make it more secure. What could you change it to?");
        questionToAdd.setContinueButton(continueButton);
        questionToAdd.setQuestionBox(questionBox);
        answerToAdd = new MultiAnswer(root);

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "H20 b@ttle",
            "waterbottle!",
            "water bottle",
            "dihydrogenmonoxide", new[] { 0, 3 },
            "\"H20 b@ttle\" cannot be found in the dictionary, and seems more random than the others. \"dihydrogenmonoxide\" can be strong because of length, but it's technically a 2-word combo."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "b0ttle o' H20",
            "WaterBottle!",
            "water_bottle",
            "twohydrogensoneoxygen", new[] { 0, 3 },
            "\"b0ttle o' H20\" cannot be found in the dictionary, and seems more random than the others. \"twohydrogensoneoxygen\" is strong because of length, but is made out of dictionary words."
        ));

        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "H20 in b0ttle",
            "WATERBOTTLE",
            "waterbottle10!",
            "justhydrogenandoxygen", new[] { 0, 3 },
            "\"H20 in b0ttle\" cannot be found in the dictionary, and seems more random than the others. \"justhydrogenandoxygen\" is also strong, but is made out of dictionary words."
        ));

        questionToAdd.setAnswer(answerToAdd);
        questions.Add(questionToAdd);

        questionToAdd = new Question("You know your co-worker writes his password down because it is very" +
                                          " complicated and hard to remember. What could you suggest to him to do instead?");
        questionToAdd.setContinueButton(continueButton);
        questionToAdd.setQuestionBox(questionBox);
        answerToAdd = new MultiAnswer(root);
        answerToAdd.addToAnswerPool(new MultiAnswerSet(
            "Password Manager",
            "Make a new password",
            "Create mnenomic",
            "Store in .txt file", new[] { 0, 1, 2 },
            "A Password Manager is secure software that handles your passwords for you. Helping your co-worker remember his password is better than leaving a password in readable form."
        ));

        questionToAdd.setAnswer(answerToAdd);
        questions.Add(questionToAdd);
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

    //team B addition
    public void onMultiAnswerButtonAny(int num)
    {

        bool correct = false;
        if(num == 1)
            correct = ((MultiAnswer)currentQuestion.getAnswer()).onButton1Clicked();
        if (num == 2)
            correct = ((MultiAnswer)currentQuestion.getAnswer()).onButton2Clicked();
        if (num == 3)
            correct = ((MultiAnswer)currentQuestion.getAnswer()).onButton3Clicked();
        if (num == 4)
            correct = ((MultiAnswer)currentQuestion.getAnswer()).onButton4Clicked();
        if(correct)
        {
            PlayerPrefs.SetInt("QuestionRight", 1);
        }
        else
        {
            PlayerPrefs.SetInt("QuestionRight", 0);
        }
        gameControl.GetComponent<GameControllerV2>().ResetGame(); 
    }



    public void displayAnswerFeedback(bool correct)
    {

        if (correct)
        {
            int random = Random.Range(0, affirmation.Length);
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

            currentQuestion.changeQuestionText(affirmation[random] + currentQuestion.getAnswer().getExplanation());

            // increase NP by 2
            GameControllerV2.Instance.IncreaseNP(2);
        }
        else
        {
            int random = Random.Range(0, disdain.Length);
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);

            GameObject.Find("scn_quiz_password").GetComponent<PasswordSceneController>().DecreaseLife();
            currentQuestion.changeQuestionText(disdain[random] + currentQuestion.getAnswer().getExplanation());
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