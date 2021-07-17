using UnityEngine;
using UnityEngine.UI;

public class OneTimePadQuizManager : MonoBehaviour {

    public static byte round = 1;

    // Answers given for questions
    public InputField encryptedAnswer;


    // 3 rounds - decryption, encryption, bonus
    public GameObject encryptedRound;
    public GameObject generalQuestion;

    // continue button
    public GameObject nextButton;

    public GameObject button_menue;// the button in the main scene that allow you to start over the game

    public GameObject squares_random; 
    public Text[] squaresT_random;

    public Text textToChange; // this is what make the difference between the questions 1 and 2

    private string encryptionAnswer; // here I will save the result for question 1 (encrypt a sentence)
    private byte[] otpTheBest = { 15, 20, 16, 20, 8, 5, 2, 5, 19, 20 }; // Saving the values of each letter
    private byte[] itsTheSame = {  9, 20, 19, 20, 8, 5, 19, 1, 13, 5 };
    private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public GameObject ECorrect;
    public GameObject EIncorrect;
    public GameObject GCorrect;
    public GameObject GIncorrect;

    public GameObject hint;
    public GameObject instructions;
    private bool trueFalse;

    public GameObject questions;

    private Dialogue dialogue; // To be able to midify the dialogue at run time
    private string [] inputForDialogue; // The sentences for the dialogue

    // starts with decryption question
    void Start()
    {
        inputForDialogue = new string[1];

        encryptedRound.SetActive(true);
        button_menue.SetActive(false);// no need for this button in the quiz
        squares_random.SetActive(true);
        generalQuestion.SetActive(false);
    }

    // proceed to next question
    public void NextSet()
    {
        switch (round)
        {
            case 1:
                
                //Make sure that there's an input to process:
                if(encryptedAnswer.textComponent.text != "" && squaresT_random[0].text != "0")
                {
                    //The sentence "otp the best" has 10 letters equivalent to the values: 15 20 16 20 8 5 2 5 19 20:
                    for (int i = 0; i < 10; i++)  //Find the right answer:  
                    {
                        if (i == 3 || i == 6)
                            encryptionAnswer += " ";
                        
                        //Check to see if I get 26 % 26 which will give 0 and 0 - 1 is array out of range
                        if(((int.Parse(squaresT_random[i].text) + otpTheBest[i]) % 26) == 0) 
                            encryptionAnswer += letters[25];// add the last letter which is Z
                        else
                            encryptionAnswer += letters[((int.Parse(squaresT_random[i].text) + otpTheBest[i]) % 26) - 1];
                    }
                    
                    // comparing on the left the user input and on the right the computer solution 
                    if (encryptedAnswer.textComponent.text.ToLower() == encryptionAnswer.ToLower()) //Answer correct:
                    {
                        // play beep sound
                        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);
                        
                        // increase NP
                        GameControllerV2.Instance.IncreaseNP(10);
                        
                        // Feedback for correct answer
                        //inputForDialogue = new string[1];// The sentence to display in the dialogue 
                        inputForDialogue[0] = "Very Good! You encrypted correctly!" +
                            "\nYour answer was: " + encryptedAnswer.textComponent.text.ToLower() + "" +
                            "\nThat's the correct answer. \nContinue to the second question.";
                        dialogue = new Dialogue { sentences = inputForDialogue };
                        
                        ECorrect.GetComponent<DialogueTrigger>().dialogue = dialogue;
                        ECorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                    }
                    else // answer is not correct:
                    {
                        byte counter = 0;
                        //comparing the two solutions (user VS computer) to see who has shortest answer:
                        byte chooseOne = encryptedAnswer.textComponent.text.ToLower().Length <= 11 ? 
                                                        (byte) encryptedAnswer.textComponent.text.ToLower().Length : (byte) 11;

                        for (byte i = 0; i < chooseOne; i++)
                        {
                            //Debug.Log(encryptedAnswer.textComponent.text.ToLower()[i] + " ---1--- " + encryptionAnswer.ToLower()[i]);
                            if (encryptedAnswer.textComponent.text.ToLower()[i] == encryptionAnswer.ToLower()[i])
                            {
                                //Debug.Log(encryptedAnswer.textComponent.text.ToLower()[i] + " ---1 S--- " + encryptionAnswer.ToLower()[i]);
                                if(encryptedAnswer.textComponent.text.ToLower()[i] != ' ' && encryptionAnswer.ToLower()[i] != ' ')
                                    counter++;
                            }
                        }
                        
                        // Decrease NP
                        GameControllerV2.Instance.DecreaseNP(counter);

                        // Feedback for incorrect answer
                        inputForDialogue[0] = "Whoops.. You weren't able to properly encrypt the text." +
                            "\nThe correct ansser is: " + encryptionAnswer.ToLower() +"" +
                            "\nYour answer was: " + encryptedAnswer.textComponent.text.ToLower() + "" +
                          "\nTry again in the next question! You got " + counter + "/10 points";
                        dialogue = new Dialogue { sentences = inputForDialogue };

                        EIncorrect.GetComponent<DialogueTrigger>().dialogue = dialogue;
                        EIncorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                    }
                    
                    // Next round
                    round++;
                    
                    //Reseting all values:
                    textToChange.text = "\"Its the same\"";
                    
                    for (int i = 0; i < 10; i++) // reset the random generator
                    {
                        squaresT_random[i].text = "0";
                    }
                    
                    encryptionAnswer = ""; // reseting the answer to check it with a different combination
                    encryptedAnswer.text = ""; 
                    
                }
                break;

            case 2:

                if (encryptedAnswer.textComponent.text != "" && squaresT_random[0].text != "")
                {
                    
                    //The sentence "otp the best" has 10 letters equivalent to the values: 15 20 16 20 8 5 2 5 19 20:
                    for (int i = 0; i < 10; i++)
                    {
                        if (i == 3 || i == 6)
                            encryptionAnswer += " ";
                        
                        //Check to see if I get 26 % 26 which will give 0 and 0 - 1 is array out of range
                        if (((int.Parse(squaresT_random[i].text) + itsTheSame[i]) % 26) == 0)
                            encryptionAnswer += letters[25];// add the last letter which is Z
                        else
                            encryptionAnswer += letters[((int.Parse(squaresT_random[i].text) + itsTheSame[i]) % 26) - 1];
                    }
                    
                    // removes potential capitalization mistakes
                    if (encryptedAnswer.textComponent.text.ToLower() == encryptionAnswer.ToLower()) //"xvywx rsfshc")
                    {
                        Debug.Log("Entering encryption round");
                        
                        // play beep sound
                        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);
                        
                        // increase NP
                        GameControllerV2.Instance.IncreaseNP(10);
                        
                        // Feedback for correct answer
                        inputForDialogue[0] = "Perfect! You encrypted correctly! You're doind a great job!" +
                            "\nYour answer was: " + encryptedAnswer.textComponent.text.ToLower() + "" +
                            "\nThat's the correct answer. \nContinue to the second question.";
                        dialogue = new Dialogue { sentences = inputForDialogue };

                        ECorrect.GetComponent<DialogueTrigger>().dialogue = dialogue;
                        ECorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                    }
                    else
                    {
                        byte counter = 0;
                        //comparing the two solutions (user VS computer) to see who has shortest answer:
                        byte chooseOne = encryptedAnswer.textComponent.text.ToLower().Length <= 11 ?
                                                        (byte)encryptedAnswer.textComponent.text.ToLower().Length : (byte)11;

                        for (byte i = 0; i < chooseOne; i++)
                        {
                            if (encryptedAnswer.textComponent.text.ToLower()[i] == encryptionAnswer.ToLower()[i])
                            {
                                if (encryptedAnswer.textComponent.text.ToLower()[i] != ' ' && encryptionAnswer.ToLower()[i] != ' ')
                                    counter++;
                            }
                        }
                        
                        // Decrease NP
                        GameControllerV2.Instance.DecreaseNP(counter);
                       /* 
                        // Feedback for incorrect answer
                        inputForDialogue[0] = "Whoops.. You weren't able to properly encrypt the text." +
                            "\nThe correct ansser is: " + encryptionAnswer.ToLower() + "" +
                            "\nYour answer was: " + encryptedAnswer.textComponent.text.ToLower() + "" +
                            "\nTry again in the next question! You got " + counter + "/10 points";
                        */
                        inputForDialogue[0] = "Whoops.. You weren't able to properly encrypt the text." +
                            "\nThe correct ansser is: " + encryptionAnswer.ToLower() + "" +
                            "\nYour answer was: " + encryptedAnswer.textComponent.text.ToLower() + "" +
                            "\nTry harder the next time but until then don't stop practice!" +
                            "\nYou got " + counter + "/10 points";
                        
                        dialogue = new Dialogue { sentences = inputForDialogue };

                        EIncorrect.GetComponent<DialogueTrigger>().dialogue = dialogue;
                        EIncorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                    }
                    
                    // Next round
                    round++;
                    encryptedRound.SetActive(false);
                    squares_random.SetActive(false);
                    nextButton.SetActive(false);
                    instructions.SetActive(false);
                    hint.SetActive(false);
                    generalQuestion.SetActive(true); // continue to the yes no questions
                }
                break;

            case 3:

                if(trueFalse) // what I get back from OTPChoiceManager is either true or false
                {
                    GCorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    GIncorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                }

                questions.SetActive(false);

                // Next scene
                GameControllerV2.Instance.scn_one_time_pad.SetActive(false);
                GameControllerV2.Instance.DisplayDecision();
                Destroy(this);

                break;
            
            default:
                Debug.Log("Default Case");
                break;
        }
    }

    public void GenerateNumbers()
    {
        encryptionAnswer = ""; // reseting the variable for each generated cycle

        for (int i = 0; i < 10; i++) // length 10
        {
            squaresT_random[i].text = "" + Random.Range(1, 26);
        }
    }

    public void setTrueFalse(bool x)
    {
        trueFalse = x;
    }
}