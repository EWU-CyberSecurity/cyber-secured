namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents a single question in a topic
    /// </summary>
    public class Question : TopicItem 
    {
        public string questionText { get; set; }
        public string questionType { get; set; }
        public string questionID { get; set; }
        public Answer answer { get; set; }

        public override void startItem()
        {
            // Here we create the question box and put the text in it.
            // And then call answer.displayAnswer()
        }
    }
}
