using System;
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

        private bool wasPlayerCorrect()
        {
            string userAnswer = fillInComponents.transform.Find("InputField").GetComponent<InputField>().text;
            return string.Equals(userAnswer, correctAnswer, StringComparison.OrdinalIgnoreCase);
        }

        public override void DisplayAnswer()
        {
            Debug.Log("displaying fill in answer");
            fillInComponents.SetActive(true);
        }

        public bool OnSubmitButtonClicked()
        {
            changeColorsAndDisableButtons();
            return wasPlayerCorrect();
        }

        protected override void changeColorsAndDisableButtons()
        {
            // Check if the text in the input field is the correct answer.
            if (wasPlayerCorrect())
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

        public override void hideAnswerComponents()
        {
            fillInComponents.SetActive(false);
        }
    }
}
