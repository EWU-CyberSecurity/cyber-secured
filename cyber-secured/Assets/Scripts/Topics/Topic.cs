﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class is the main container for an entire topic,
    /// which consists of dialogue, and questions. Dialogue and
    /// questions are subclasses of TopicItem
    /// </summary>
    public class Topic
    {
        private int itemNumber;
        private List<TopicItem> topicItems;
        private Dialogue startDialogue;
        private string topicID;
        private string topicName;
        private TopicItem currentItem;


        public Topic(string topicID, string topicName,
            Dialogue startDialogue)
        {
            this.topicID = topicID;
            this.topicName = topicName;
            this.startDialogue = startDialogue;
            this.topicItems = new List<TopicItem>();
        }

        public void start()
        {
            itemNumber = 0;
            Debug.Log("starting the first topic");
            startDialogue.startItem();
            Debug.Log("first item is dialogue? " + (topicItems[itemNumber] is Dialogue));
            if (topicItems[itemNumber] is Dialogue) return;

            currentItem = topicItems[itemNumber];

            currentItem.startItem();
        }

        public void AddQuestion(Question question)
        {
            Debug.LogWarning("Adding " + question + " to List<Questions> question");
            this.topicItems.Add(question);
        }

        public void AddDialogue(Dialogue dialogue)
        {
            this.topicItems.Add(dialogue);
        }

        public bool trueFalseButtonClicked(bool trueWasClicked)
        {
            // the current item must be a question instead of dialogue.
            Question currentQuestion = (Question) currentItem;
            currentQuestion.showContinueButton();
            // we can assume this is a true false answer because of this button being clicked.
            return ((TFAnswer) currentQuestion.getAnswer()).OnTrueFalseButtonClicked(trueWasClicked);
        }

        // when we allow dialogue in the middle of a topic this should be nextItem()
        public void nextQuestion()
        {
            // this checks for instance type and also creates a variable that is currentItem cast to Question
            if (currentItem is Question currentQuestion)
            {
                currentQuestion.hideAnswerComponents();
            }
            else
            {
                Debug.LogError("dialogue in the middle of a quiz isn't supported yet.");
            }

            itemNumber++;
            if (itemNumber == topicItems.Count)
            {
                Debug.Log("move on to the next month...");
            }
            else
            {
                currentItem = topicItems[itemNumber];
                currentItem.startItem();
            }
        }

        public string getName()
        {
            return topicName;
        }
    }
}
