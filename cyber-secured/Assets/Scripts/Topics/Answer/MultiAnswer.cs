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

        public MultiAnswer()
        {
            answerPool = new List<MultiAnswerSet>();
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

            GameObject root = GameObject.Find("Quiz Components").transform.Find("Buttons").gameObject;
            GameObject button1 = root.transform.Find("multi_answer_btn_1").gameObject;
            GameObject button2 = root.transform.Find("multi_answer_btn_2").gameObject;
            GameObject button3 = root.transform.Find("multi_answer_btn_3").gameObject;
            GameObject button4 = root.transform.Find("multi_answer_btn_4").gameObject;

            button1.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(0);
            button2.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(1);
            button3.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(2);
            button4.transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(3);


        }

        // When the button is clicked check if it belongs to the right answer
        public void onButton1Clicked()
        {

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
