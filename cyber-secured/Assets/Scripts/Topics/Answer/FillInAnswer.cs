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

        public GameObject fillInComponents;

        public FillInAnswer(string sheetCorrectAnswer)
        {
            fillInComponents = quizComponentsRoot.transform.Find("fillin_components").gameObject;
            this.correctAnswer = sheetCorrectAnswer;
        }

        //Makes a empty string for the userAnswer, finds the InputField in the Stage object for FillIn, and gets the text then returns the UserAnswer.
        string InputFieldInput()
        {
            string userAnswer = " ";
            userAnswer = fillInComponents.transform.Find("InputField").GetComponent<InputField>().text;
            return userAnswer;
        }

        bool checkUIAnswer(string userAnswer)
        {
            if (userAnswer.ToLower().Equals(correctAnswer))
                return true;
            else
                return false;
        }

        public override void DisplayAnswer()
        {
            Debug.Log("displaying fill in answer");
            fillInComponents.SetActive(true);
        }

        public void OnContinueButtonClicked()
        {
            string userAnswer = InputFieldInput();

            // Check if the text in the input field is the correct answer.
            if (checkUIAnswer(userAnswer))
            {
                //get next question or the dialogue.
                fillInComponents.transform.Find("InputField").GetComponent<InputField>().selectionColor = disabledCorrectColor;

            }
            else
            {
                //Tell the user they entered the wrong answer, or continue on with the game.
                fillInComponents.transform.Find("InputField").GetComponent<InputField>().selectionColor = disabledIncorrectColor;

            }
        }

        protected override void changeColorsAndDisableButtons()
        {
            
        }

        public override void hideAnswerComponents()
        {
            fillInComponents.SetActive(false);
        }
    }
}
