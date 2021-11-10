using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents a pool of multiple choice answers,
    /// which is a list of MultiAnswerSets. One of these sets will be
    /// displayed.
    /// </summary>
    class MultiAnswer : Answer
    {
        private List<MultiAnswerSet> answerPool; // all the possible answer sets
        private MultiAnswerSet displayedSet; // the set of answers that is actually displayed
        private Color disabledCorrectColor = new Color(0.5764706f, 1, 0.5882353f);
        private Color disabledIncorrectColor = new Color(1, 0.5764706f, 0.5764706f);

        private GameObject button1;
        private GameObject button2;
        private GameObject button3;
        private GameObject button4;

        public MultiAnswer()
        {
            answerPool = new List<MultiAnswerSet>();

            GameObject root = GameObject.Find("Quiz Components").transform.Find("Buttons").gameObject;
            this.button1 = root.transform.Find("multi_answer_btn_1").gameObject;
            this.button2 = root.transform.Find("multi_answer_btn_2").gameObject;
            this.button3 = root.transform.Find("multi_answer_btn_3").gameObject;
            this.button4 = root.transform.Find("multi_answer_btn_4").gameObject;
        }

        public MultiAnswer(List<MultiAnswerSet> answerPool)
        {
            this.answerPool = answerPool;
        }

        public void addToAnswerPool(MultiAnswerSet answerSetToAdd)
        {
            answerPool.Add(answerSetToAdd);
        }

        public override void displayAnswer()
        {
            // Set the text on the four multiple choice buttons. 
            displayedSet = answerPool.ElementAt(Random.Range(0, answerPool.Count));

            button1.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(0);
            button2.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(1);
            button3.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(2);
            button4.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(3);
        }

        // When the button is clicked check if it belongs to the right answer
        public void onButton1Clicked()
        {
            if (displayedSet.isCorrectAnswer(0))
            {
                ColorBlock colors = button1.GetComponent<Button>().colors;
                colors.disabledColor = disabledCorrectColor;
            }
        }

        public void onButton2Clicked()
        {

        }

        public void onButton3Clicked()
        {

        }

        public void onButton4Clicked()
        {

        }
    }
}
