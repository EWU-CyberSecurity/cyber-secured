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

        public override void displayFeedback(bool correct)
        {
            if (correct)
            {
                questionBoxText.text = correct_feedback_template.Replace("[answer]", correctAnswer);
            }
            else
            {
                questionBoxText.text = incorrect_feedback_template.Replace("[answer]", correctAnswer);
            }
        }

        public override void DisplayAnswer()
        {
            fillInComponents.SetActive(true);
            fillInComponents.transform.Find("submit_btn").gameObject.GetComponent<Button>().interactable = true;
        }

        public bool OnSubmitButtonClicked()
        {
            changeColorsAndDisableButtons();
            displayFeedback(wasPlayerCorrect());
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

            fillInComponents.transform.Find("submit_btn").gameObject.GetComponent<Button>().interactable = false;
        }

        public override void hideAnswerComponents()
        {
            fillInComponents.SetActive(false);
        }
    }
}
