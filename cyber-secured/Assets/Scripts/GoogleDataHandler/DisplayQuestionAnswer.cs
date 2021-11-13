using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestionAnswer : MonoBehaviour
{
    // As the sheet row actually starts at the second row, row 0 is headers so we can skip that.

    // How to change the text in button 1, GameObject.Find("btn1").GetComponentInChildren<Text>().text = "la di da";
    [SerializeField] public Text questionsPrompt;
    [SerializeField] public Button btn1;
    [SerializeField] public Button btn2;
    [SerializeField] public Button btn3;
    [SerializeField] public Button btn4;

    private string QuestionPrompt = "What is a virus";
    private string Answer1 = "A malicious piece of code";
    private string Answer2 = "A youtube video";
    private string Answer3 = "A book";
    private string Answer4 = "a email";
    
    public void ReceivingLists()
    {

    }

    public void Update()
    {
        DisplayMultiChoiceQuestion(QuestionPrompt, Answer1, Answer2,Answer3, Answer4);
        DisplayFillInQuestion(QuestionPrompt, Answer1, Answer2, Answer3, Answer4);
        DisplayTrueFalseQuestion(QuestionPrompt, Answer1, Answer2);
    }
    
    public void DisplayMultiChoiceQuestion(string questionProm, string ans1, string ans2, string ans3, string ans4)
    {
        GameObject.Find("text_question").GetComponentInChildren<Text>().text = questionProm;
        // Parse through answers to which is the correct one(s)

        GameObject.Find("btn1").GetComponentInChildren<Text>().text = ans1;
        GameObject.Find("btn2").GetComponentInChildren<Text>().text = ans2;
        GameObject.Find("btn3").GetComponentInChildren<Text>().text = ans3;
        GameObject.Find("btn4").GetComponentInChildren<Text>().text = ans4;
    }

    public void DisplayTrueFalseQuestion(string questionProm, string ans1, string ans2)
    {
        GameObject.Find("text_question").GetComponentInChildren<Text>().text = questionProm;

        // Parse through answers to which is the correct one(s)

        GameObject.Find("btn1").GetComponentInChildren<Text>().text = ans1;
        GameObject.Find("btn2").GetComponentInChildren<Text>().text = ans2;
    }

    public void DisplayFillInQuestion(string questionProm, string ans1, string ans2, string ans3, string ans4)
    {
        GameObject.Find("text_question").GetComponentInChildren<Text>().text = questionProm;

        // Parse through answers to which is the correct one(s)
        // GameObject.Find("btn1").GetComponentInChildren<Text>().text = ans1;   ******* TODO figure out how to change the text in a field that the user can enter values from (like in the password quiz)


        if (ans1 != null) {
            
        } else if (ans2 != null) {
            
        } else if (ans3 != null) {
            
        } else if (ans4 != null) {
            
        }


        /* Check if there is more then one answer(in case its just a single answer they have to fill in)
         * Take in their input for each spot and check it against the answers(might have to loop through all answers in case the player didn't input it in the same slot.
         *  ^ set to all answers to lowercase then check between the table data and UserInput < (Loop through their choices to make sure they have each word even if word is not in the same column as table)
         */

    }
}
