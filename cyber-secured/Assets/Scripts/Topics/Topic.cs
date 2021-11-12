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
        public List<Question> questions = new List<Question>();

        public void AddQuestion(Question q)
        {
            Debug.LogWarning("Adding " + q + " to List<Questions> question");
            this.questions.Add(q);
        }

        public string TopicID { get; set; }
        public string TopicName { get; set; }
        public Dialogue start { get; set; }
        public Dialogue end { get; set; }

        public void nextItem()
        {
            // Proceed to the next topic item in the list
        }
    }
}
