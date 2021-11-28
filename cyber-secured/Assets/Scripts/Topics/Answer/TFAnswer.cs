
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
        GameObject tf_components = GameObject.Find("stage_custom_topics").transform.Find("Quiz Components").transform.Find("true_false_components").gameObject;
        Button True_btn;
        Button False_btn;

        public bool OnTrueFalseButtonClicked(bool trueWasClicked)
        {
            return trueWasClicked == isTrue;
        }

        public override void DisplayAnswer()
        {
            tf_components.SetActive(true);
        }
    }
}
