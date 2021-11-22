
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
        public bool isTrue { get; set; }
        bool isClicked;
        GameObject tf_components = GameObject.Find("stage_custom_topics").transform.Find("Quiz Components").transform.Find("true_false_components").gameObject;
        Button True_btn;
        Button False_btn;

        void TaskOnTrueClick()
        {
            isClicked = true;
            Debug.LogWarning("True was pressed");
        }

        void TaskOnFalseClick()
        {
            isClicked = false;
            Debug.LogWarning("False was pressed");
        }

        public override void displayAnswer()
        {
            tf_components.SetActive(true);
        }

        private void OnContinueButtonClicked()
        {
            True_btn.onClick.AddListener(TaskOnTrueClick);
            False_btn.onClick.AddListener(TaskOnFalseClick);

            if (isClicked)
            {
                base.displayCorrectDialogue();
            }
            else
            {
                base.displayIncorrectDialogue();
            }
        }
    }
}
