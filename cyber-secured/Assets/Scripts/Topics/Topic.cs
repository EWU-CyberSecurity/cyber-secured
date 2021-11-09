using System.Collections.Generic;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class is the main container for an entire topic,
    /// which consists of dialogue, and questions. Dialogue and
    /// questions are subclasses of TopicItem
    /// </summary>
    class Topic
    {
        public List<TopicItem> items { get; set; }
        public string TopicID { get; set; }
        public string TopicName { get; set; }

        public void nextItem()
        {
            // Proceed to the next topic item in the list
        }
    }
}
