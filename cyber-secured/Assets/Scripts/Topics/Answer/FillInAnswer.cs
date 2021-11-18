using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents an answer where you have to write something
    /// in a text box.
    /// </summary>
    class FillInAnswer : Answer
    {
        private string correctAnswer;

        private List<MultiAnswerSet> answerPool; // all the possible answer sets
        private MultiAnswerSet displayedSet; // the set of answers that is actually displayed
        private Color disabledCorrectColor = new Color(0.5764706f, 1, 0.5882353f);
        private Color disabledIncorrectColor = new Color(1, 0.5764706f, 0.5764706f);

        public GameObject stage_FillIn;

        public FillInAnswer(string sheetCorrectAnswer)
        {
            this.correctAnswer = sheetCorrectAnswer;
        }

        //Makes a empty string for the userAnswer, finds the InputField in the Stage object for FillIn, and gets the text then returns the UserAnswer.
        string InputFieldInput()
        {
            string userAnswer = " ";
            userAnswer = stage_FillIn.transform.Find("InputField").GetComponent<InputField>().text;
            return userAnswer;
        }

        bool checkUIAnswer(string userAnswer)
        {
            if (userAnswer.ToLower().Equals(correctAnswer))
                return true;
            else
                return false;
        }

        public override void displayAnswer()
        {
            // Move the input field and continue button (plus anything else that's needed)
            // to the right place. There is an example of this kind of thing happening in
            // the caesar cipher quiz.

            //GameObject.Find("text_question").GetComponentInChildren<Text>().text = ;
            //question_prompt.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(0);


        }

        public void OnContinueButtonClicked()
        {
            // Check if the text in the input field is the correct answer.

            //check it against the correct answer
            //pass if correct, try again if failed.
            string userAnswer = InputFieldInput();
            if (checkUIAnswer(userAnswer))
            {
                //get next question or the dialogue.
            }
            else
            {
                //Tell the user they entered the wrong answer, or continue on with the game.
            }
        }
    }
}
