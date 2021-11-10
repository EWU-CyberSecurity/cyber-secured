using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents a single question in a topic
    /// </summary>
    class Question : TopicItem
    {
        private string question_text;
        private Answer answer;

        public Question(string question_text)
        {
            this.question_text = question_text;
        }
        public Question(string questionText, Answer answer)
        {
            this.question_text = questionText;
            this.answer = answer;
        }

        public void setAnswer(Answer answer)
        {
            this.answer = answer;
        }

        public Answer getAnswer()
        {
            return this.answer;
        }

        public override void startItem()
        {
            // Here we create the question box and put the text in it.
            // And then call answer.displayAnswer()
            GameObject root = GameObject.Find("Quiz Components");
            GameObject question_box = root.transform.Find("question_box").gameObject;
            question_box.transform.Find("Text").GetComponent<Text>().text = question_text;

            answer.displayAnswer();
        }
    }
}
