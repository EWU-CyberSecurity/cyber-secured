using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents a single question in a topic
    /// </summary>
    public class Question : TopicItem
    {
        private GameObject questionBox = GameObject.Find("Canvas").transform.Find("stage_custom_topics").transform.Find("question_box").gameObject;
        private GameObject continueButton = GameObject.Find("Canvas").transform.Find("stage_custom_topics").transform.Find("Continue").gameObject;
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
            // And then call answer.DisplayAnswer()
            hideContinueButton();

            changeQuestionText(questionText);
            showQuestionBox();

            answer.DisplayAnswer();
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

        public void showQuestionBox()
        {
            questionBox.SetActive(true);
        }

        public void hideQuestionBox()
        {
            questionBox.SetActive(false);
        }

        public void setQuestionBox(GameObject questionBox)
        {
            this.questionBox = questionBox;
        }

        public void setContinueButton(GameObject continueButton)
        {
            this.continueButton = continueButton;
        }

        // hide all of the components associated with the current answer.
        public void hideAnswerComponents()
        {
            answer.hideAnswerComponents();
        }
    }
}
