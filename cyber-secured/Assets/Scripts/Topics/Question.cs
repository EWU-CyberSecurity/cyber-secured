using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents a single question in a topic
    /// </summary>
    class Question : TopicItem
    {
        private GameObject questionBox = GameObject.Find("Quiz Components").transform.Find("question_box").gameObject;
        private GameObject continueButton = GameObject.Find("Quiz Components").transform.Find("Continue_button").gameObject;
        private string questionText;
        private Answer answer;

        public string questionType { get; set; }
        public string questionID { get; set; }

        public Question(string question_text)
        {
            this.questionText = question_text;
        }
        public Question(string questionText, Answer answer)
        {
            this.questionText = questionText;
            this.answer = answer;
        }

        public string getQuestionText()
        {
            return this.questionText;
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
            hideContinueButton();

            changeQuestionText(questionText);

            answer.displayAnswer();
        }

        public void changeQuestionText(string newText)
        {
            questionBox.transform.Find("Text").GetComponent<Text>().text = newText;
        }

        public void showContinueButton()
        {
            continueButton.SetActive(true);
        }

        public void hideContinueButton()
        {
            continueButton.SetActive(false);
        }
    }
}
