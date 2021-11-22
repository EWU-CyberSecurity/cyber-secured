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
            Debug.Log("starting the first topic!");
            startDialogue.startItem();
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

        public string getName()
        {
            return topicName;
        }
    }
}
