﻿using System.Collections.Generic;
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

        private GameObject root = GameObject.Find("Quiz Components").transform.Find("Buttons").gameObject;

        private GameObject button1;
        private GameObject button2;
        private GameObject button3;
        private GameObject button4;

        // use this for updating all
        private GameObject[] buttons;

        public MultiAnswer()
        {
            answerPool = new List<MultiAnswerSet>();

            button1 = root.transform.Find("multi_answer_btn_1").gameObject;
            button2 = root.transform.Find("multi_answer_btn_2").gameObject;
            button3 = root.transform.Find("multi_answer_btn_3").gameObject;
            button4 = root.transform.Find("multi_answer_btn_4").gameObject;

            // use this for updating all the buttons in a clean way
            buttons = new GameObject[] { button1, button2, button3, button4 };
    }

        public MultiAnswer(List<MultiAnswerSet> answerPool) : this()
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
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.Find("Text").GetComponent<Text>().text = displayedSet.getAnswer(i);
            }
        }

        // When the button is clicked check if it belongs to the right answer
        public bool onButton1Clicked()
        {
            changeColorsAndDisableButtons();
            return displayedSet.isAnswerCorrect(0);
        }

        private void changeColorsAndDisableButtons()
        {
            // Change the disabled colors on the answer buttons depending
            // on whether or not they were correct.
            for (int i = 0; i < buttons.Length; i++)
            {
                ColorBlock newColors = button1.GetComponent<Button>().colors;
                newColors.disabledColor = displayedSet.isAnswerCorrect(i) ? disabledCorrectColor : disabledIncorrectColor;
                buttons[i].GetComponent<Button>().colors = newColors;
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }

        public bool onButton2Clicked()
        {
            changeColorsAndDisableButtons();
            return displayedSet.isAnswerCorrect(1);
        }

        public bool onButton3Clicked()
        {
            changeColorsAndDisableButtons();
            return displayedSet.isAnswerCorrect(2);
        }

        public bool onButton4Clicked()
        {
            changeColorsAndDisableButtons();
            return displayedSet.isAnswerCorrect(3);
        }
    }
}
