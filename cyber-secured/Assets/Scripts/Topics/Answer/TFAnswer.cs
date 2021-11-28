
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents an answer that is either true or false,
    /// and therefore has 2 buttons.
    /// </summary>
    class TFAnswer : Answer
    {
        private bool isTrue;
        private GameObject tfRoot;
        Button btn_true;
        Button btn_false;
        public TFAnswer(bool isTrue)
        {
            tfRoot = quizComponentsRoot.transform.Find("true_false_components").gameObject;
            this.isTrue = isTrue;

            btn_true = tfRoot.transform.Find("btn_true").GetComponent<Button>();
            btn_false = tfRoot.transform.Find("btn_false").GetComponent<Button>();
        }

        public bool OnTrueFalseButtonClicked(bool trueWasClicked)
        {
            changeColorsAndDisableButtons();
            return trueWasClicked == isTrue;
        }

        public override void DisplayAnswer()
        {
            tfRoot.SetActive(true);
        }

        protected override void changeColorsAndDisableButtons()
        {
            ColorBlock newTrueButtonColors = btn_true.colors;
            ColorBlock newFalseButtonColors = btn_false.colors;

            newTrueButtonColors.disabledColor = isTrue ? disabledCorrectColor : disabledIncorrectColor;
            newFalseButtonColors.disabledColor = isTrue ? disabledIncorrectColor : disabledCorrectColor;

            btn_true.colors = newTrueButtonColors;
            btn_false.colors = newFalseButtonColors;

            btn_true.interactable = false;
            btn_false.interactable = false;
        }
    }
}
