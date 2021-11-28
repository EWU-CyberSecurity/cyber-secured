using System.Collections.Generic;
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
            Debug.Log("starting the first topic");
            startDialogue.startItem();
            Debug.Log("first item is dialogue? " + (topicItems[0] is Dialogue));
            if (topicItems[0] is Dialogue) return;

            currentItem = topicItems[0];

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

        public string getName()
        {
            return topicName;
        }
    }
}
